using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionState : State
{

    List<TileLogic> tiles;

    public override void Enter()
    {
        base.Enter();
        inputs.OnMove += OnMoveTileSelector;
        inputs.OnFire += OnFire;
        tiles = Board.instance.Search(Turn.unit.tile);
        tiles.Remove(Turn.unit.tile);
        Board.instance.SelectTiles(tiles, Turn.unit.alliance);
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        inputs.OnFire -= OnFire;
        Board.instance.DeselectTiles(tiles);
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;


        if (button == 1)
        {
            //So permite o personagem se mover a um Tile dentre os encontrados via Dijkstra
            if(tiles.Contains(machine.selectedTile))
                machine.ChangeTo<MoveSequenceState>();

        }
        else if (button == 2)
        {
            machine.ChangeTo<ChooseActionState>();

        }
    }



}
