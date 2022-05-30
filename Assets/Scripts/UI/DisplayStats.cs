using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public TextMeshProUGUI charNameText;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI maxHPText;
    public TextMeshProUGUI movementText;
    public TextMeshProUGUI ataqueText;

    private void OnEnable()
    {
        TurnBeginState.OnNewTurn += UpdateUI;
    }

    void OnDisable()
    {
        TurnBeginState.OnNewTurn -= UpdateUI;
    }

    public void UpdateUI(Unit turnUnit) 
    {
   
        charNameText.text = turnUnit.name;
        HPText.text = turnUnit.GetStat(StatEnum.HP).ToString();
        maxHPText.text = turnUnit.GetStat(StatEnum.MaxHP).ToString();

        movementText.text = turnUnit.GetStat(StatEnum.MOVE).ToString();
        ataqueText.text = turnUnit.GetStat(StatEnum.ATK).ToString();

    }



}
