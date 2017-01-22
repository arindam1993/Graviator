using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiplayerWithBindingsExample;

[RequireComponent(typeof(Rigidbody2D))]
public class GraviatorPlayer : MonoBehaviour {

    public int PlayerIndex = -1;
    public PlayerActions Actions;

    //Movement parameters
    public float RotateSpeed;
    public float MoveSpeed;

    //Fuel parameters
    public float MinFuel;
    public float MaxFuel;
    public float StartFuel;
    public float FuelSpendRate;
    public float FuelRegenRate;
    private float currentFuel;

    public SpriteRenderer[] characterSprites;

    //Gun
    public Gun gun;

    public JetTrail jetTrail;

    //Private member variables
    Rigidbody2D rbd;
    BoxCollider2D col;

    public enum ControlMode
    {
        StickOnly,
        Trigger
    }
    public ControlMode controlMode;

    public GameObject PistolBulletPrefab;

    EnergyBar eB;


    public bool Invulnerable = false;
    public bool Dead = false;


    // Use this for initialization
    void Start () {
        rbd = GetComponent<Rigidbody2D>();

        foreach (SpriteRenderer s in characterSprites)
        {
            s.color *= ( PlayerColorDict.GetPlayerColor(PlayerIndex) + new Color(0, 0.8f, 0.8f, 0));
            Debug.Log(s.color);
        }

        Reset();

        if(rbd == null)
        {
            rbd = gameObject.AddComponent<Rigidbody2D>();
        }
        
        rbd.drag = 0.3f;
        gun.PlayerIndex = PlayerIndex;
        eB = UIManager.Instance.energyBars[PlayerIndex];

        eB.SetColor(PlayerColorDict.GetPlayerColor(PlayerIndex));

        eB.Show();

	}


    private void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Invulnerable");
        Invulnerable = true;

        StartCoroutine(flicker());

        UnityTimer.Instance.CallAfterDelay(() => {
            Invulnerable = false;
            gameObject.layer = LayerMask.NameToLayer("Player"+(PlayerIndex+1));
        }, 3.0f);

        currentFuel = StartFuel;

        this.transform.position = SpawnPoints.Instance.GetSpawnPoint(PlayerIndex);
        Dead = false;
    }

	
	// Update is called once per frame
	void Update () {

        if (!Dead)
        {

            float thrustMag = 0.0f;
            if (controlMode == ControlMode.StickOnly) thrustMag = Actions.Rotate.Vector.magnitude;
            if (controlMode == ControlMode.Trigger) thrustMag = Actions.LT.RawValue;

            RotateTowards(Actions.Rotate.Vector);
            if (currentFuel > 0.2)
            {
                Thrust(thrustMag);
                jetTrail.SetThrust(thrustMag);
            }

            if (thrustMag < 0.1)
            {
                if (currentFuel < MaxFuel)
                {
                    RegenFuel();
                }
            }

            gun.SetAimDirection(Actions.Aim.Vector);

            if (!Invulnerable)
            {
                if (Actions.RT.WasPressed)
                {
                    gun.FireDown();
                }

                if (Actions.RT.IsPressed)
                {
                    gun.FireHeld();
                }
            }

            eB.SetPlayerData(MaxFuel, currentFuel);
        }
	}


    void RotateTowards(Vector3 orientation)
    {
        if (orientation.magnitude > 0.01) {
            Quaternion target = Quaternion.LookRotation(Vector3.forward, orientation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, RotateSpeed * Time.deltaTime);
        }
         
    }

    void Thrust(float thrust)
    {
        rbd.AddRelativeForce(Vector3.up * MoveSpeed * thrust, ForceMode2D.Force);
        currentFuel -= FuelSpendRate * thrust * Time.deltaTime;
    }

    void RegenFuel()
    {
        currentFuel += FuelRegenRate * Time.deltaTime;
    }

    public void TakeHit(Vector2 incident, float intensity)
    {
        rbd.AddForce(incident * intensity , ForceMode2D.Impulse);

        float newFuel = MaxFuel - intensity * 5;
        MaxFuel = Mathf.Clamp(newFuel   , MinFuel, 10000);

        currentFuel = Mathf.Clamp(currentFuel, 0, MaxFuel);


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GravityWell")
        {
            rbd.velocity *= 0.5f;
            rbd.angularVelocity *= 0.5f;
        }


        if(collision.gameObject.tag == "Hazard")
        {
            foreach (SpriteRenderer s in characterSprites)
            {
                s.color *= new Color(1, 1, 1, 0);
            }
            Dead = true;
            UnityTimer.Instance.CallAfterDelay(() =>
           {
               Reset();
           }, 2.0f);

            ScoreManager.Instance.RemoveDeathScore(PlayerIndex);
            
        }

        if (collision.gameObject.tag == "Item")
        {
            PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();
            gun.SetPowerupFireable(powerUp.firePowerUp.GetComponent<IFireable>());
            powerUp.HideItem();
            powerUp.itemFeedback.Animate(transform, 1, 0, powerUp.expirationTime);
        }
    }


    IEnumerator flicker()
    {
        while (Invulnerable)
        {
            foreach(SpriteRenderer s  in characterSprites)
            {
                s.color *= new Color(1, 1, 1, 0);
            }
           
            yield return new WaitForSeconds(0.2f);
            foreach (SpriteRenderer s in characterSprites)
            {
                s.color += new Color(0, 0, 0, 1);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }


  
    
}
