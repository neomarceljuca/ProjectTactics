using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISearcher
{
    List<TileLogic> Search(TileLogic start);
}
