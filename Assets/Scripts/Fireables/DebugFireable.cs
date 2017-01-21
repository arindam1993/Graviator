using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFireable : MonoBehaviour, IFireable {


    public void Initialize(OnFireableExpiredDelegate cb, Transform shootPt)
    {
        Debug.Log("Fireable Initialized");
        this.transform.parent = shootPt;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
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
