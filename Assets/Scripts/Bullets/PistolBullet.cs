using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;
public class PistolBullet : MonoBehaviour {

    public float LifeTime = 5.0f;

    public TrailRenderer trail;
    public GameObject sprite;
    public ParticleSystem pS;
    public float KnockbackIntensity;
   

    bool otherDestroyed;
    
    void Start()
    {
       
    }

    public void OnDisable()
    {
        Debug.Log("Pistol Bullet despawned");
        trail.Clear();
       
    }

    public void OnEnable()
    {
        Debug.Log("Spawn Called");

        otherDestroyed = false;

        sprite.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
        pS.Play();

        Rigidbody2D rbd = GetComponent<Rigidbody2D>();
        

        UnityTimer.Instance.CallAfterDelay(() =>
        {
            EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
        }, 1.0f);

        if( collision.gameObject.tag == "Player")
        {
            GraviatorPlayer player = collision.gameObject.GetComponent<GraviatorPlayer>();
            player.TakeHit(
                    Vector3.Normalize(rbd.velocity),
                    KnockbackIntensity
                );
        }

        rbd.velocity = Vector2.zero;
        rbd.angularVelocity = 0;
        rbd.bodyType = RigidbodyType2D.Static;


    }



    // Update is called once per frame
    void Update () {
	}
}
