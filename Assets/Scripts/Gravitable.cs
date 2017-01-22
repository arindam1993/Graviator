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

            // Get Planet Gravity Well Properties
            GravityWell gravityWell = planet.GetComponent(typeof(GravityWell)) as GravityWell;
            float planetGravDist = gravityWell.gravDist;
            float planetGravity = gravityWell.gravity;

//            Debug.Log("Planet: " + planet.gameObject.name + ". GravDist: " + planetGravDist + ". Gravity: " + planetGravity);

            if (dist <= planetGravDist) {
                Vector3 v = planet.transform.position - transform.position;
                rb.AddForce(v.normalized  * maxGravity / (dist * dist));
// =======
//                 rb.AddForce(v.normalized * (1.0f - dist / planetGravDist) * planetGravity);
// >>>>>>> origin/custom_gravity_wells
            }
        }
    }
}
