using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(AddUnitDelay());
    }

    IEnumerator AddUnitDelay()
    {
        Turn.unit.chargeTime += 300;
        if (Turn.hasMoved)
            Turn.unit.chargeTime += 100;
        if (Turn.hasActed)
            Turn.unit.chargeTime += 100;
        Turn.unit.chargeTime -= Turn.unit.GetStat(StatEnum.SPEED);

        Turn.hasActed = Turn.hasMoved = false;
        machine.units.Remove(Turn.unit);
        machine.units.Add(Turn.unit);
        yield return new WaitForSeconds(0.5f);
        machine.ChangeTo<TurnBeginState>();
    }
}
