using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	
    public void SetAimDirection(Vector2 direction)
    {
        if (direction.magnitude < 0.5) return;
        transform.up = Vector3.Normalize(direction);
    }
}
