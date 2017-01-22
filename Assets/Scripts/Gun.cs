using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    //Default fireable fires when you have no powerup fireable
    public GameObject PistolFireable;
    public GameObject MGFireable;
    public int PlayerIndex = -1;
    IFireable defaultFireable;
    IFireable powerUpFireable;

    public Transform firePoint;

    public void SetPowerupFireable(IFireable fa)
    {
        this.powerUpFireable = fa;
        fa.Initialize(OnFireableExpired, firePoint, PlayerIndex);      
    }

    public void FireDown()
    {
        if (powerUpFireable != null)
        {
            powerUpFireable.OnFireDown();
            return; 
        }
        defaultFireable.OnFireDown();
    }
	
    public void FireHeld()
    {
        if (powerUpFireable != null)
        {
            powerUpFireable.OnFireHeld();
            return;
        }
        defaultFireable.OnFireHeld();
    }
    public void SetAimDirection(Vector2 direction)
    {
        if (direction.magnitude < 0.5) return;
        transform.up = Vector3.Normalize(direction);
    }

    public void OnFireableExpired()
    {
        Debug.Log("Fireable expired");
        this.powerUpFireable = null;
    }



    //public DebugFireable debugFireable;
    ////Debug
    void Awake()
    {
        this.powerUpFireable = null;
    }

    void Start()
    {
        //debugFireable = GameObject.Find("DebugFireable").GetComponent<DebugFireable>();
        this.defaultFireable = PistolFireable.GetComponent<IFireable>();
        this.defaultFireable.Initialize(null, firePoint, PlayerIndex);

    }
}
