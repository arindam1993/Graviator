using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorDict  {

    static Color[] lookup = { Color.red , Color.blue , Color.green , Color.yellow  };

	public static Color GetPlayerColor(int playerIndex)
    {
        return lookup[playerIndex];    
    }
}
