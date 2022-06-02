using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackCalcDamage : MonoBehaviour
{
    public Skill mySkill;


    void OnEnable()
    {
        TurnBeginState.OnNewTurn += UpdateSkillDamage;

    }

    private void OnDisable()
    {
        TurnBeginState.OnNewTurn -= UpdateSkillDamage;
    }

    private void UpdateSkillDamage(Unit turnUnit)
    {
        mySkill.damage = turnUnit.GetStat(StatEnum.ATK);
    }


}
