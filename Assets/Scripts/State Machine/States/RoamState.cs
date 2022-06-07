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
        State.lookToTile += displayStatsMenu;
        inputs.OnFire += OnFire;
        CheckNullPosition();

        machine.characterStatsPanel.MoveTo("Show");
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        inputs.OnFire -= OnFire;
        State.lookToTile -= displayStatsMenu;

        //ensure updates to correct display
        machine.myStatDisplayer.UpdateUI(Turn.unit);
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



    void displayStatsMenu(TileLogic t) 
    {
        machine.characterStatsPanel.MoveTo("Hide");
        if (t.content != null) 
        {
            var mycontent = t.content.GetComponent<Unit>();
            if (mycontent is Unit) 
            {
                machine.myStatDisplayer.UpdateUI(mycontent);
                machine.characterStatsPanel.MoveTo("Show");
            }
            else 
            {
            
            }

        }
    }

   


}
