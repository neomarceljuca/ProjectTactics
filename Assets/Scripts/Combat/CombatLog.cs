using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatLog
{
    static List<string> theLog = new List<string>();

    public static void CheckAtive()
    {
        foreach (Unit u in StateMachineController.instance.units)
        {
            if (u.GetStat(StatEnum.HP) <= 0)
                u.active = false;
            else
                u.active = true;
        }
    }

    public static bool IsOver()
    {
        int activeAlliances = 0;
        for (int i = 0; i < MapLoader.instance.alliances.Count; i++)
        {
            activeAlliances += CheckAlliance(MapLoader.instance.alliances[i]);
        }
        if (activeAlliances > 1)
        {
            return false;
        }
        return true;
    }

    public static int CheckAlliance(Alliance alliance)
    {
        for (int i = 0; i < alliance.units.Count; i++)
        {
            Unit currentUnit = alliance.units[i];
            if (currentUnit.active)
            {
                return 1;
            }
        }
        return 0;

    }



    public static void UpdateLog(string newLog)
    {
        theLog.Add(newLog);

    }

    public static void ShowLog()
    {
        foreach (string str in theLog)
        {
            //print str
            Debug.Log(str); 
        }

    }

}
