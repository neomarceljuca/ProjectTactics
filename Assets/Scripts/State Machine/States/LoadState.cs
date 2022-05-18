using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : State
{
    public override void Enter()
    {
        StartCoroutine(LoadSequence());
    }

    IEnumerator LoadSequence()
    {
        yield return StartCoroutine(Board.instance.InitSequence(this));
        //
        yield return null;
        MapLoader.instance.CriaUnidades();
        yield return null;

        InitialTurnOrdering();
        yield return null;
        UnitAlliances();
        yield return null;

        List<Vector3Int> blockers = Blockers.instance.GetBlockers();
        yield return null;
        SetBlockers(blockers);
        yield return null;
        //
        StateMachineController.instance.ChangeTo<TurnBeginState>();
    }

    void InitialTurnOrdering()
    {
        for (int i = 0; i < machine.units.Count; i++)
        {
            machine.units[i].chargeTime = 100 - machine.units[i].GetStat(StatEnum.SPEED); //Onde a velocidade afeta a ordem nos turnos, provavelmente modificar
            machine.units[i].active = true;
        }
    }

    void UnitAlliances()
    {
        for (int i = 0; i < machine.units.Count; i++)
        {
            SetUnitAlliance(machine.units[i]);
        }
    }

    void SetUnitAlliance(Unit unit)
    {
        for (int i = 0; i < MapLoader.instance.alliances.Count; i++)
        {
            if (MapLoader.instance.alliances[i].factions.Contains(unit.faction))
            {
                MapLoader.instance.alliances[i].units.Add(unit);
                unit.alliance = i;
                return;
            }
        }

    }

    void SetBlockers(List<Vector3Int> blockers) 
    {
        foreach (Vector3Int pos in blockers) 
        {
            TileLogic t = Board.GetTile(pos);
            t.content = Blockers.instance.gameObject;
        }
    }

}
