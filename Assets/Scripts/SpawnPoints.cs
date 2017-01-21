using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoints : MonoBehaviour
{

    public static SpawnPoints Instance;

    private Vector2[] spawnPoints;

    void Awake()
    {
        Debug.Log("Init");
        Instance = this;
        spawnPoints = new Vector2[4];
    }

    public Vector2 GetSpawnPoint(int playerIndex)
    {
        return spawnPoints[playerIndex];
    }

    void Start()
    {
        //Setup the spawnpoints
        int i = 0;
        foreach(Transform child in transform)
        {
            Debug.Log(i);
            spawnPoints[i] = child.position;
            i++;
        }
    }

}
