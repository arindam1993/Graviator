using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float minSize, maxSize;
    GameObject[] players;

    public Transform topRight, bottomLeft;
    float distance;
    float initialCameraSize;

    public float speed;

	void Start ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        distance = Vector3.Distance(topRight.position, bottomLeft.position);

        initialCameraSize = Camera.main.orthographicSize;
	}
	
	void Update ()
    {
        float minX, minY, maxX, maxY;
        minX = players[0].transform.position.x;
        minY = players[0].transform.position.y;
        maxX = players[0].transform.position.x;
        maxY = players[0].transform.position.y;

        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].transform.position.x < minX)
            {
                minX = players[i].transform.position.x;
            }
            if (players[i].transform.position.y < minY)
            {
                minY = players[i].transform.position.y;
            }
            if (players[i].transform.position.x > maxX)
            {
                maxX = players[i].transform.position.x;
            }
            if (players[i].transform.position.y > maxY)
            {
                maxY = players[i].transform.position.y;
            }
        }

        float newSize = Vector3.Distance(new Vector3(minX, minY, 0), new Vector3(maxX, maxY, 0)) * Camera.main.orthographicSize / distance;
        if (newSize < minSize) newSize = minSize;
        if (newSize > maxSize) newSize = maxSize;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newSize, Time.deltaTime * speed);

        Vector3 centerOfPlayers = Vector3.zero;
        for (int i = 0; i < players.Length; i++)
        {
            centerOfPlayers += players[i].transform.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, centerOfPlayers / players.Length, Time.deltaTime * speed);

        float posX = 0, posY = 0;
        if (transform.position.x < minSize - initialCameraSize)
        {
            posX = minSize - initialCameraSize;
        }
        if (transform.position.x > initialCameraSize - maxSize)
        {
            posX = initialCameraSize - maxSize;
        }
        if (transform.position.y < minSize - initialCameraSize)
        {
            posY = minSize - initialCameraSize;
        }
        if (transform.position.y > initialCameraSize - maxSize)
        {
            posX = initialCameraSize - maxSize;
        }

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
