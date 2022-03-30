﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<Stat> stats; 



    void Awake()
    {
        stats = new List<Stat>();
        for (int i = 0; i < 10 ; i++)
        {
            Stat temp = new Stat();
            temp.type = (StatEnum)i;
            stats.Add(temp);
        }
    }

}
