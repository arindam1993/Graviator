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

    //Gun
    public Gun gun;

    public JetTrail jetTrail;

    //Private member variables
    Rigidbody2D rbd;

    public enum ControlMode
    {
        StickOnly,
        Trigger
    }
    public ControlMode controlMode;

    public GameObject PistolBulletPrefab;


    // Use this for initialization
    void Start () {
        rbd = GetComponent<Rigidbody2D>();
        if(rbd == null)
        {
            rbd = gameObject.AddComponent<Rigidbody2D>();
        }
        currentFuel = StartFuel;

        rbd.drag = 0.3f;
        gun.PlayerIndex = PlayerIndex;

	}
	
	// Update is called once per frame
	void Update () {

        float thrustMag=0.0f;
        if (controlMode == ControlMode.StickOnly) thrustMag = Actions.Rotate.Vector.magnitude;
        if (controlMode == ControlMode.Trigger) thrustMag = Actions.LT.RawValue;

        RotateTowards(Actions.Rotate.Vector);
        if ( currentFuel > 0 )
        {
            Thrust(thrustMag);
        }

        if ( thrustMag < 0.1 )
        {
            if( currentFuel < StartFuel)
            {
                RegenFuel();
            }
        }

        gun.SetAimDirection(Actions.Aim.Vector);

        if(Actions.RT.WasPressed)
        {
            gun.FireDown();
        }

        if (Actions.RT.IsPressed)
        {
            gun.FireHeld();
        }


        jetTrail.SetThrust(thrustMag);
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
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GravityWell")
        {
            rbd.velocity *= 0.5f;
            rbd.angularVelocity *= 0.5f;
        }

        if (collision.gameObject.tag == "Item")
        {
            PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();
            gun.SetPowerupFireable(powerUp.firePowerUp.GetComponent<IFireable>());
            powerUp.HideItem();
        }
    }



}
