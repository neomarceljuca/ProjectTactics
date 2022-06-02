﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [HideInInspector]
    public Stats stats;

    public int faction;

    public int alliance;

    public TileLogic tile;

    public int chargeTime;

    public bool active;

    void Awake()
    {
        stats = GetComponentInChildren<Stats>();
    }

    public int GetStat(StatEnum stat)
    {
        return stats.stats[(int)stat].value;

    }

    public void SetStat(StatEnum stat, int value)
    {
        //TO DO impedir que um personagem se cure além da vida máxima

        stats.stats[(int)stat].value = GetStat(stat) + value;
    }

}
