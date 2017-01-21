using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The thing that attracts
public class Attractor : MonoBehaviour
   {
       public float range;
       public float mass; // not really a mass, but whatever


       public Vector3 GetLocalEffect(Vector3 position)
       {
           Vector3 delta = position - transform.position;
           if (delta.sqrMagnitude > range * range)
               return Vector3.zero;

           float percentage = (range  -  delta.magnitude) / range;

           return -delta.normalized * percentage * percentage * mass;
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           Debug.Log(gameObject.name + " Attractor has entered " + other.gameObject.name);

       }
   }