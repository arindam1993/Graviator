using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class JetTrail : MonoBehaviour {

    ParticleSystem pS;
    ParticleSystem.MainModule _mM;

    public float minLifeTime = 0.2f;
    public float maxLifeTime = 1.0f;

    float MIN_THRUST = 0.01f;

	// Use this for initialization
	void Start () {
        pS = GetComponent<ParticleSystem>();
        _mM = pS.main;
        pS.Stop();
        _mM.startLifetime = minLifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetThrust(float thrust)
    {
        //Start the particle system if its off initially
        if( thrust > MIN_THRUST)
        {
            if (pS.isStopped) pS.Play();
        }else
        {
            if (pS.isPlaying) pS.Stop();
        }



        //Stop particle system if thrust is too low

        float lifeTime = minLifeTime + (maxLifeTime - minLifeTime) * thrust * 0.4f;
        _mM.startLifetime = lifeTime;
    }
}
