using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// the thing being attracted
public class Mover : MonoBehaviour
{
    private Vector3 velocity;

    public Attractor[] attractors;

    private void Update()
    {
        foreach(Attractor attractor in attractors)
            velocity += attractor.GetLocalEffect(transform.position);

        transform.position += (velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object that has entered Attractor Gravity well " + other.gameObject.name);

    }
}
