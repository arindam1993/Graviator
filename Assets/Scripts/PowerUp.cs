using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject firePowerUp;

    public void HideItem()
    {
        // temporary "hiding"
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
