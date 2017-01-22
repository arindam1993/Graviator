using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class MachineGunFireable : MonoBehaviour, IFireable
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float shootVelocity;
    int PlayerIndex;

    [SerializeField]
    float timeToExpire = 5;

    [SerializeField]
    float delayBetweenShots = .2f;
    float timeSinceLastShot = 0;


    public AudioClip[] shootSounds;
    AudioSource shootAud;

    public void Initialize(OnFireableExpiredDelegate cb, Transform firePoint, int PlayerIndex)
    {
        this.transform.parent = firePoint;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.PlayerIndex = PlayerIndex;


        UnityTimer.Instance.CallAfterDelay(()=> {
            cb();
        }, timeToExpire);
    }

    public void OnFireDown()
    {
        Shoot();
    }

    public void OnFireHeld()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > delayBetweenShots)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = EasyObjectPool.instance.GetObjectFromPool("MachineGunBullet", this.transform.position, Quaternion.identity);
        Rigidbody2D rbd = bullet.GetComponent<Rigidbody2D>();
        bullet.layer = LayerMask.NameToLayer("Bullet_Player" + (PlayerIndex + 1));
        rbd.velocity = this.transform.up * shootVelocity;

        SoundFXManager.RandomizeSfx(shootAud, shootSounds);
        MachineGunBullet mb = bullet.GetComponent<MachineGunBullet>();
        if (mb != null) {
            mb.trail.startColor = PlayerColorDict.GetPlayerColor(PlayerIndex);
            mb.PlayerIndex = PlayerIndex;   
        }

       

        timeSinceLastShot = 0;
    }

    void Start()
    {
        shootAud = GetComponent<AudioSource>();
    }

    void Update()
    {

    }
}
