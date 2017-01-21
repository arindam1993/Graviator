using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFireable : MonoBehaviour, IFireable {


    public void Initialize(OnFireableExpiredDelegate cb)
    {
        Debug.Log("Fireable Initialized");
    }

    public void OnFireDown()
    {
        //Debug.Log("Fireable pressed");
    }

    public void OnFireHeld()
    {
        Debug.Log("fireable held");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
