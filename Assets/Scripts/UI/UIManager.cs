using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {



    public static UIManager Instance;
    public List<EnergyBar> energyBars;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach(EnergyBar b in energyBars)
        {
            b.Hide();
        }
    }

}
