using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitable : MonoBehaviour {

    public float maxGravDist = 4.0f;
    public float maxGravity = 35.0f;

    private Rigidbody2D rb;

    private GameObject[] _planets;

    void Start () {
        _planets = GameObject.FindGameObjectsWithTag("GravityWell");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        foreach(GameObject planet in _planets) {
            float dist = Vector3.Distance(planet.transform.position, transform.position);

            // Get Planet Gravity Well Properties
            GravityWell gravityWell = planet.GetComponent(typeof(GravityWell)) as GravityWell;
            float planetGravDist = gravityWell.gravDist;
            float planetGravity = gravityWell.gravity;

//            Debug.Log("Planet: " + planet.gameObject.name + ". GravDist: " + planetGravDist + ". Gravity: " + planetGravity);

            if (dist <= planetGravDist) {
                Vector3 v = planet.transform.position - transform.position;
                rb.AddForce(v.normalized  * planetGravity / (dist * dist));

            }
        }
    }
}
