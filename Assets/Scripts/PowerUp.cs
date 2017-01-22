using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject firePowerUp;
    public ItemFeedback itemFeedback;
    public float expirationTime;

    public void HideItem()
    {
        //gameObject.SetActive(false);
        // temporary "hiding"
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //fade ideally
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
