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

        distance = topRight.position.y - bottomLeft.position.y; //Vector3.Distance(topRight.position, bottomLeft.position);

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

        float newSize = Vector3.Distance(new Vector3(minX, minY, 0), new Vector3(maxX, maxY, 0)) * initialCameraSize / distance;
        if (newSize < minSize) newSize = minSize;
        if (newSize > maxSize) newSize = maxSize;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newSize, Time.deltaTime * speed);

        Vector3 centerOfPlayers = Vector3.zero;
        for (int i = 0; i < players.Length; i++)
        {
            centerOfPlayers += players[i].transform.position;
        }
        centerOfPlayers = centerOfPlayers / players.Length;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(centerOfPlayers.x, centerOfPlayers.y, -10), Time.deltaTime * speed);

        float posX = 0, posY = 0;
        if (transform.position.x < minSize - maxSize)
        {
            posX = minSize - maxSize;
        }
        if (transform.position.x > maxSize - minSize)
        {
            posX = maxSize - minSize;
        }
        if (transform.position.y < minSize - maxSize)
        {
            posY = minSize - maxSize;
        }
        if (transform.position.y > maxSize - minSize)
        {
            posY = maxSize - minSize;
        }

        transform.position = new Vector3(posX, posY, -10);
    }
}
