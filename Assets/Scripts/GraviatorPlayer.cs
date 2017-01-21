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
    public float currentFuel;

    //Gun
    public Gun gun;

    //Private member variables
    Rigidbody2D rbd;

    public enum ControlMode
    {
        StickOnly,
        Trigger
    }
    public ControlMode controlMode;


    // Use this for initialization
    void Start () {
        rbd = GetComponent<Rigidbody2D>();
        if(rbd == null)
        {
            rbd = gameObject.AddComponent<Rigidbody2D>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        float thrustMag=0.0f;
        if (controlMode == ControlMode.StickOnly) thrustMag = Actions.Rotate.Vector.magnitude;
        if (controlMode == ControlMode.Trigger) thrustMag = Actions.RT.RawValue;

        RotateTowards(Actions.Rotate.Vector);
        if ( currentFuel > 0 )
        {
            Thrust(thrustMag);
        }

        gun.SetAimDirection(Actions.Aim.Vector);
	}


    void RotateTowards(Vector3 orientation)
    {
        Debug.Log(orientation);
        Quaternion target = Quaternion.LookRotation(Vector3.forward, orientation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, RotateSpeed * Time.deltaTime);
    }

    void Thrust(float thrust)
    {
        rbd.AddRelativeForce(Vector3.up * MoveSpeed * thrust, ForceMode2D.Force);
        currentFuel -= FuelSpendRate * thrust * Time.deltaTime;
    }




}
