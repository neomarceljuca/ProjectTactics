using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempCharacterDisplayUI : MonoBehaviour
{
    int hp;
    Image myWindow;

    private void Awake()
    {
        myWindow = GetComponent<Image>();
    }



    private void OnEnable()
    {
        TurnBeginState.OnNewTurn += changeUIColor;
    }

    void OnDisable()
    {
        TurnBeginState.OnNewTurn -= changeUIColor;
    }


    private void changeUIColor(int factionID)
    {
        if (factionID == 0)
        {
            //Debug.Log("UI Blue!");
            myWindow.color = new Color32(32, 126, 255, 125);
        }
        else if (factionID == 1)
        {
            //Debug.Log("UI Red!"); 
            myWindow.color = new Color32(255, 71, 35, 125);
        }
    }

}
