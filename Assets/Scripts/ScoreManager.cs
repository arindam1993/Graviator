using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance;

    int[] scores;

    void Awake()
    {
        Instance = this;
        scores = new int[4]; 
    }


    public void AddHitScore(int player)
    {
        scores[player] += 10;
        UIManager.Instance.energyBars[player].SetScore(scores[player]);
    }

    public void RemoveDeathScore(int player)
    {
        scores[player] -= 50;
        UIManager.Instance.energyBars[player].SetScore(scores[player]);
    }
}
 