using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseActionState : State
{

    public override void Enter()
    {
        MoveSelector(Turn.unit.tile);
        base.Enter();
        index = 0;
        currentUISelector = machine.chooseActionSelection;
        ChangeUISelector(machine.chooseActionButtons);
        CheckActions();
        inputs.OnMove += OnMove;
        inputs.OnFire += OnFire;

        //teste
        inputs.TestOnPress += TestOnPress;
        
        machine.chooseActionPanel.MoveTo("Show");
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMove;
        inputs.OnFire -= OnFire;

        //teste
        inputs.TestOnPress -= TestOnPress;

        machine.chooseActionPanel.MoveTo("Hide");
    }

    void OnMove(object sender, object args)
    {
        Vector3Int button = (Vector3Int)args;
        if (button == Vector3Int.left)
        {
            index--;

            ChangeUISelector(machine.chooseActionButtons);
        }
        else if(button == Vector3Int.right)
        {
            index++;
            ChangeUISelector(machine.chooseActionButtons);
        }

    }


    void OnFire(object sender, object args)
    {
        int button = (int)args;


        if (button == 1)
        {
            ActionButtons();

        }
        else if(button == 2)
        {
            machine.ChangeTo<RoamState>();

        }
    }

    //test
    void TestOnPress(object sender, object args)
    {
        int button = (int)args;

        if (button == 1)
        {
            CombatLog.ShowLog();
        }
    }


    void ActionButtons()
    {
        switch (index)
        {
            case 0:
                if(!Turn.hasMoved)
                    machine.ChangeTo<MoveSelectionState>();
                break;
            case 1:
                //if (!Turn.hasActed)
                    //machine.ChangeTo<SkillSelectionState>();
                break;
            case 2:
                //if (!Turn.hasActed)
                    // machine.ChangeTo<ItemSelectState>();
                    break;
            case 3:
                machine.ChangeTo<TurnEndState>();
                break;
        }
    }

    void CheckActions()
    {
        PaintButton(machine.chooseActionButtons[0], Turn.hasMoved); //se se moveu
        //--temporariamente desativar ações--
        PaintButton(machine.chooseActionButtons[1], true);
        PaintButton(machine.chooseActionButtons[2], true);
        //--


        //PaintButton(machine.chooseActionButtons[1], Turn.hasActed); //se agiu
        //PaintButton(machine.chooseActionButtons[2], Turn.hasActed); // se usou item; conta como acao tbm

    }

    void PaintButton(Image image, bool check)
    {
        if (check)
        {
            image.color = Color.gray;
        }
        else
        {
            image.color = Color.white;
        }
    }


    
}
