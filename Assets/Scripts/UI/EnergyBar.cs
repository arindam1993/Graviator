using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

    public Image totalBar;
    public Image currentBar;
    public Text Score;



    public void SetPlayerData(float maxFuel,float currentFuel, int score )
    {
        float tbHeight = totalBar.rectTransform.sizeDelta.y;
        float cbHeight = currentBar.rectTransform.sizeDelta.y;
        totalBar.rectTransform.sizeDelta = new Vector2(maxFuel, tbHeight);
        currentBar.rectTransform.sizeDelta = new Vector2(currentFuel, cbHeight);

        Score.text = score + "";

    }


    public void SetColor(Color playerColor)
    {
        currentBar.color = playerColor;
    }


    public void Show()
    {
        totalBar.gameObject.SetActive(true);
        currentBar.gameObject.SetActive(true);
        Score.gameObject.SetActive(true);
    }

    public void Hide()
    {
        totalBar.gameObject.SetActive(false);
        currentBar.gameObject.SetActive(false);
        Score.gameObject.SetActive(false);
    }



}
