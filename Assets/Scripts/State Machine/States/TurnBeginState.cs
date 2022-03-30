using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBeginState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(SelectUnit());
    }

    IEnumerator SelectUnit()
    {
        BreakDraw(); 
        machine.units.Sort((x,y) => x.chargeTime.CompareTo(y.chargeTime));
        Turn.unit = machine.units[0];

        yield return null;
        machine.ChangeTo<ChooseActionState>();

    }


    void BreakDraw() //vulgo desempate
    {
        for (int i=0; i < machine.units.Count-1; i++ )
        {
            if (machine.units[i].chargeTime == machine.units[i + 1].chargeTime) //criterio de desempate
            {
                machine.units[i + 1].chargeTime += 1;
            }
        }
    }


}
