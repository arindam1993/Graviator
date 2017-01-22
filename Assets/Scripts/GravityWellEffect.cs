using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof (GravityWell))]
public class GravityWellEffect : MonoBehaviour
{

    public float targetScale = 0.5f;
    public float ShrinkSpeed = 0.6f;
    public float FadeSpeed = 0.04f;
    public float startingAlpha = 0.15f;
    public float targetAlpha = 1.0f;
    public float fadeMultiplyer = 1.0f;

    private float currentScale;
    private float planetSize;
    private Vector3 startingScale;
    private Transform parentTransform;
    private float currentAlpha;
    private Color spriteColor;
    private Transform _transform;

    private GravityWell _gravityWell;
    private float _gravDist;

	// Use this for initialization
	void Start ()
	{
	    // Scale GravityWellEffect radius according to GravityWell gravDist
	    _gravDist = GetComponentInParent<GravityWell>().gravDist / (1.2f * transform.parent.localScale.x);
	    currentScale = _gravDist;
	    transform.localScale = new Vector3(currentScale, currentScale, currentScale);

	    // Set Starting Alpha
	    currentAlpha = startingAlpha;

	    spriteColor = gameObject.GetComponent<SpriteRenderer>().color;
	    spriteColor.a = currentAlpha;
	    gameObject.GetComponent<SpriteRenderer>().color = spriteColor;

	}

	// Update is called once per frame
	void Update ()
	{
	    // Update Scale over time
	    currentScale -= ShrinkSpeed * Time.deltaTime;
	    transform.localScale = new Vector3(currentScale, currentScale, currentScale);
	    if (currentScale < targetScale)

	    {
	        currentScale = _gravDist;
	    }

	    // Update Alpha over time
	    currentAlpha -= FadeSpeed * Time.deltaTime;
        spriteColor.a = currentAlpha;
	    gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
	    if (currentAlpha <= targetAlpha)
	    {
	        spriteColor = gameObject.GetComponent<SpriteRenderer>().color;
	        spriteColor.a = startingAlpha;
	        gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
	        currentAlpha = startingAlpha;
	    }


	}
}
