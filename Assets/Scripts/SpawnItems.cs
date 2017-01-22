using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject item;

    public List<SpriteRenderer> planets;
    public float minTime, maxTime;
    float lastSpawnTime;
    float currentSpawnTimeframe;
    public int numOfSpawnPoints;
    GameObject lastInstantiatedObj;

	void Start ()
    {
        lastSpawnTime = Time.time;
        currentSpawnTimeframe = Random.Range(minTime, maxTime);
    }
	
	void Update ()
    {
		if (Time.time - lastSpawnTime > currentSpawnTimeframe)
        {
            lastSpawnTime = Time.time;
            currentSpawnTimeframe = Random.Range(minTime, maxTime);

            SpriteRenderer sr = planets[Random.Range(0, planets.Count)];

            int spawnPoint = Random.Range(1, numOfSpawnPoints + 1);
            float angle = (360 / numOfSpawnPoints) * spawnPoint * Mathf.Deg2Rad;  // no zero please =(

            float x = sr.transform.lossyScale.x / 2;
            float y = sr.transform.lossyScale.y / 2;
            float newX = x * Mathf.Cos(angle) - y * Mathf.Sin(angle);
            float newY = x * Mathf.Sin(angle) + y * Mathf.Cos(angle);

            Destroy(lastInstantiatedObj);
            lastInstantiatedObj = GameObject.Instantiate(item, new Vector2(sr.transform.position.x + newX, sr.transform.position.y + newY), Quaternion.identity);
        }
	}
}
