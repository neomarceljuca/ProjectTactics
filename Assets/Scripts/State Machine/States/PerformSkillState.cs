using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerformSkillState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(PerformSequence());

    }

    IEnumerator PerformSequence()
    {
        yield return null;
        Turn.targets = Turn.skill.GetTargets();
        yield return null;
        Turn.skill.Effect();
        yield return null;
        //aqui iria qualquer efeito ao final do turno


        CombatLog.CheckAtive();
        yield return new WaitForSeconds(1);
        if (CombatLog.IsOver())
        {
            string temp = "Acabou!";
            CombatLog.UpdateLog(temp);
            Debug.Log(temp);
            EndMatch();

        }
        else
        {
            machine.ChangeTo<TurnEndState>();
        }
       
    }

    public void EndMatch() 
    {
        Time.timeScale = 0;
        TextMeshProUGUI myText = machine.gameOverPanel.GetComponentInChildren<TextMeshProUGUI>();
        myText.text = Turn.unit.alliance == 0 ? "O jogador 1 Venceu" : "O jogador 2 Venceu";
        machine.gameOverPanel.SetActive(true);
    
    }
}

