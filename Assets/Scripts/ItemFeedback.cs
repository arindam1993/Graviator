using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemFeedback : MonoBehaviour 
{
    Image image;
    float initialTime = 0;

    public void Animate(Transform player, float from, float to, float duration)
    {
        StartCoroutine(FillingAnimation(player, from, to, duration));
    }

	void Start ()
    {
        image = GetComponent<Image>();
        image.fillAmount = 0;
    }

    IEnumerator FillingAnimation(Transform player, float from, float to, float duration)
    {
        image.fillAmount = from;
        initialTime = Time.time;
        while (Time.time - initialTime < duration)
        {
            transform.position = player.position;
            image.fillAmount = Mathf.Lerp(from, to, (Time.time - initialTime) / duration);
            yield return new WaitForFixedUpdate();
        }
    }
}
