using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;
public class MachineGunBullet : MonoBehaviour
{
    public float LifeTime = 5.0f;

    public TrailRenderer trail;
    public GameObject sprite;


    bool otherDestroyed;

    void Start()
    {

    }

    public void OnDisable()
    {
        //Debug.Log("Pistol Bullet despawned");
        trail.Clear();

    }

    public void OnEnable()
    {
        //Debug.Log("Spawn Called");

        otherDestroyed = false;

        sprite.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = true;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;

        UnityTimer.Instance.CallAfterDelay(() =>
        {
            EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
        }, 1.0f);

    }
}
