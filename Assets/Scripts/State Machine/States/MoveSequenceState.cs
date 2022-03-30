using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(MoveSequence());
    }


    IEnumerator MoveSequence()
    {
        List<TileLogic> path = CreatePath();

        Movement movement = Turn.unit.GetComponent<Movement>();

        yield return StartCoroutine(movement.Move(path));
        Turn.unit.tile.content = null;
        Turn.unit.tile = machine.selectedTile;
        Turn.unit.tile.content = Turn.unit.gameObject;
        yield return new WaitForSeconds(0.2f);
        Turn.hasMoved = true;
        machine.ChangeTo<ChooseActionState>();

    }

    List<TileLogic> CreatePath()
    {
        List<TileLogic> path = new List<TileLogic>();

        TileLogic t = machine.selectedTile;
        while (t != Turn.unit.tile)
        {
            path.Add(t);
            t = t.prev;
        }

        path.Reverse();
            
    
        return  path; 
    }

}
