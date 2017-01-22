using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvailableUI : MonoBehaviour {

    public int PlayerIndex;
    bool joined = false;

    Text text;
	// Use this for initialization
	void Start () {
        Color playerColor = PlayerColorDict.GetPlayerColor(PlayerIndex);
        text = GetComponent<Text>();

        text.color *= playerColor;

        StartCoroutine(joinWatch());

        
	}
	
    IEnumerator joinWatch()
    {
        while (!joined)
        {
            foreach (GraviatorPlayerManager.PlayerData pd in GraviatorPlayerManager.instance.joinedPlayers)
            {
                if (pd.PlayerIndex == PlayerIndex)
                {
                    Debug.Log("Joined Detected");
                    text.color += new Color(0, 0, 0, 1);
                    joined = true;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
