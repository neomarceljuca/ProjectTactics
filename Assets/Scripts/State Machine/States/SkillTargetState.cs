using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetState : State
{
    public override void Enter()
    {
        base.Enter();
        inputs.OnMove += OnMoveTileSelector;
        inputs.OnFire += OnFire;
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        inputs.OnFire -= OnFire;
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;


        if (button == 1)
        {
            //checar se eh valido
            if (Turn.skill.ValidadeTarget())
                machine.ChangeTo<PerformSkillState>();

        }
        else if (button == 2)
        {
            machine.ChangeTo<SkillSelectionState>();

        }
    }
}
