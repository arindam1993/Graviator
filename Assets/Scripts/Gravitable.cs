using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitable : MonoBehaviour {

    public float maxGravDist = 4.0f;
    public float maxGravity = 35.0f;

    private Rigidbody2D rb;

    GameObject[] planets;

    void Start () {
        planets = GameObject.FindGameObjectsWithTag("GravityWell");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        foreach(GameObject planet in planets) {
            float dist = Vector3.Distance(planet.transform.position, transform.position);
            if (dist <= maxGravDist) {
                Vector3 v = planet.transform.position - transform.position;
                rb.AddForce(v.normalized  * maxGravity / (dist * dist));
            }
        }
    }
}