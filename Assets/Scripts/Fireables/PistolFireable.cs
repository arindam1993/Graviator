using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class PistolFireable : MonoBehaviour, IFireable {

    public GameObject bulletPrefab;

    public float shootVelocity;

    public void Initialize(OnFireableExpiredDelegate cb, Transform firePoint)
    {
        this.transform.parent = firePoint;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    public void OnFireDown()
    {
        GameObject bullet = EasyObjectPool.instance.GetObjectFromPool("PistolBullet", this.transform.position, Quaternion.identity);
        Rigidbody2D rbd = bullet.GetComponent<Rigidbody2D>();

        rbd.velocity = this.transform.up * shootVelocity;
    }

    //Pistol only fires on trigger press, keep this empty
    public void OnFireHeld()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
