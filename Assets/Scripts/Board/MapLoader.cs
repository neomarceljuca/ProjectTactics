using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    public Unit unitPrefab;

    //job
    //objetos no mapa
    //localizacao das unidades do mapa

    public static MapLoader instance;

    GameObject holder;
    public List<Alliance> alliances;
    
    void Awake()
    {
        instance = this;
        holder = new GameObject("Units Holder");
    }


    private void Start()
    {
        holder.transform.parent = Board.instance.transform;
        InitializeAlliances();
    }

    void InitializeAlliances()
    {
        for (int i = 0; i < alliances.Count; i++)
        {
            alliances[i].units = new List<Unit>();
        }
    }


    public void CriaUnidades()
    {
        Unit unit1 = Createunit(new Vector3Int(3 ,5 ,0 ), "Jogador 1");
        Unit unit2 = Createunit(new Vector3Int(2, 0, 0), "Jogador 2");

        StateMachineController.instance.units.Add(unit1); //lista de unidadess
        StateMachineController.instance.units.Add(unit2);

        unit1.faction = 0;
        unit2.faction = 1;

        
        unit2.GetComponentInChildren<SpriteRenderer>().color = Color.red; //apenas para diferenciar inimigo e jogador (temporario)
    }


    public Unit Createunit(Vector3Int pos, string name)
    {
        TileLogic t = Board.GetTile(pos);
        Unit unit = Instantiate(unitPrefab, t.worldPos, Quaternion.identity, holder.transform);
        unit.tile = t;
        unit.name = name;
        t.content = unit.gameObject;


        for (int i = 0; i < unit.stats.stats.Count; i++)
        {
            if(unit.stats.stats[i].type == StatEnum.MOVE)
            {   
                unit.stats.stats[i].value = Random.Range(3, 8);
            }
            else if(unit.stats.stats[i].type == StatEnum.MaxHP)
            {
                unit.stats.stats[i].value = unit.GetStat(StatEnum.HP);
            }
            else
            { 
                unit.stats.stats[i].value = Random.Range(1,100);
            }
        }

        return unit;
    }

}
