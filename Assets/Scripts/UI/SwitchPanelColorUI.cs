using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanelColorUI : MonoBehaviour
{
    Image myWindow;

    private void Awake()
    {
        myWindow = GetComponent<Image>();
    }


    public void changeUIColor(Unit turnUnit)
    {
        int factionID = turnUnit.faction;

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
