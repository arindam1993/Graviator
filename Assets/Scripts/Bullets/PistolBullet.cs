using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;
public class PistolBullet : MonoBehaviour {

    public float LifeTime = 5.0f;
    float startTime;
    public TrailRenderer trail;

    
    public void OnDisable()
    {
        Debug.Log("Pistol Bullet despawned");
        trail.Clear();
       
    }

    public void OnEnable()
    {
        Debug.Log("Spawn Called");
        startTime = Time.time;
        //trail.time = 1;

        UnityTimer.Instance.CallAfterDelay(() =>
        {
            EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
        }, LifeTime);
    }


    
	
	// Update is called once per frame
	void Update () {
	}
}
