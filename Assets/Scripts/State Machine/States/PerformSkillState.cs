﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else
        {
            machine.ChangeTo<TurnEndState>();
        }
       
    }
}

