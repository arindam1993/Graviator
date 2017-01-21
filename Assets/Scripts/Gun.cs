using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    IFireable fireable;

    public void SetFireable(IFireable fa)
    {
        this.fireable = fa;
        fa.Initialize(OnFireableExpired);      
    }

    public void FireDown()
    {
        fireable.OnFireDown();
    }
	
    public void FireHeld()
    {
        fireable.OnFireHeld();
    }
    public void SetAimDirection(Vector2 direction)
    {
        if (direction.magnitude < 0.5) return;
        transform.up = Vector3.Normalize(direction);
    }

    public void OnFireableExpired()
    {
        Debug.Log("Fireable expired");
    }



    public DebugFireable debugFireable;
    //Debug
    void Start()
    {
        debugFireable = GameObject.Find("DebugFireable").GetComponent<DebugFireable>();
        SetFireable(debugFireable);
    }
}
