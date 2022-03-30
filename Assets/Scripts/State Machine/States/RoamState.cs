using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    public override void Enter()
    {
        base.Enter();
        //add inputs
        inputs.OnMove += OnMoveTileSelector;
        inputs.OnFire += OnFire;
        CheckNullPosition();
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        inputs.OnFire -= OnFire;
    }

    void CheckNullPosition()
    {
        if (Selector.instance.tile == null)
        {
            TileLogic t = Board.GetTile(new Vector3Int(0, 0, 0));   
            Selector.instance.tile = t;
            Selector.instance.spriteRenderer.sortingOrder = t.contentOrder;
            Selector.instance.transform.position = t.worldPos;
        }
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;


        if (button == 1)
        {


        }
        else if (button == 2)
        {
            machine.ChangeTo<ChooseActionState>();

        }
    }


}
