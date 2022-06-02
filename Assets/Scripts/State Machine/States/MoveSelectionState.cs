using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionState : State, ISearcher
{

    List<TileLogic> tiles;


    //Acesso do mapa:
    //COORDENADAS X e Y: tiles( Vector3(x,y,0) )
    //COORDENADA Z para ALTURA: floor
    //Método de Dijkstra - Search()
    public Dictionary<Vector3Int, TileLogic> reachedTiles;
    public List<Floor> floors;
    [HideInInspector]
    public Vector3Int[] dirs = new Vector3Int[4] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

    public override void Enter()
    {
        base.Enter();
        inputs.OnMove += OnMoveTileSelector;
        inputs.OnFire += OnFire;

        reachedTiles = Board.instance.tiles;
        tiles = Search(Turn.unit.tile);
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


    //GRAFOS    
    //Dijkstra para pegar tiles proximas de acordo com o a distância de movement
    public List<TileLogic> Search(TileLogic start)
    {
        #region Setup
        //dist[]: contém a distância a partir da origem TileLogic start, está guardada nas informações de cada tile, ou seja:
        // dist[] = tiles.Values.distance

        //prev[]: contém o tile predecessor para o menor caminho possível.
        //prev[] = tiles.Values.prev 

        Movement m = Turn.unit.GetComponent<Movement>();
        float requiredCost;

        List<TileLogic> tilesSearch = new List<TileLogic>();
        //tilesSearch.Add(start);

        //dist[v] = infinity, prev[v] = undefined
        ClearSearch();

        // Q = todos os nodos do grafo
        //// TO DO: Implementar Min Heap em outra classe para substituir checkNext e checkNow por uma só priority Queue
        //Queue<TileLogic> checkNext = new Queue<TileLogic>();
        //Queue<TileLogic> checkNow = new Queue<TileLogic>();

        List<TileLogic> ListaPrioritaria = new List<TileLogic>();
        //ListaPrioritaria.Add(start);
        foreach (TileLogic t in reachedTiles.Values)
        {
            ListaPrioritaria.Add(t);
        }

        //equivalente a dist[source] = 0;
        start.distance = 0;
        //checkNow.Enqueue(start);
        #endregion


        //While Q not empty
        //while (checkNow.Count > 0)
        while (ListaPrioritaria.Count > 0)
        {
            ListaPrioritaria.Sort();
            TileLogic t = ListaPrioritaria[0];
            ListaPrioritaria.RemoveAt(0);

            //TileLogic t = checkNow.Dequeue();
            //Olha para as 4 direções adjacentes, contidas em dirs. Lembrando que tiles é um dicionário com chave = Vector3
            for (int i = 0; i < 4; i++)
            {
                //next = cada tile (vértice) adjacente a ser iterada e comparada com seus caminhos
                TileLogic next = GetTile(t.pos + dirs[i]);

                requiredCost = t.distance + t.movementCost + floorDifferenceCost(t, next);

                //nao adcionar se
                if (next == null || next.distance <= requiredCost || requiredCost > Turn.unit.GetStat(StatEnum.MOVE) || m.ValidateMovement(t, next))
                {
                    continue;
                }


                next.distance = requiredCost;
                next.prev = t;

                // checkNext.Enqueue(next);
                //ListaPrioritaria.Add(next);

                /* OBSERVAÇÃO
                 * adiciona aquele tile na lista de objetos alcancaveis
                 * um relaxamento de aresta pode ocasionar em tiles repetidos serem adicionados.
                 * No entanto, isso não é um problema crítico, pois essa lista de tiles é somente usada para ser selecionada e atualizada na tela
                 * Logo, não há problema alguns tiles serem atualizados e selecionados mais de uma vez, o conjunto final não será alterado.
                 */
                tilesSearch.Add(next);
            }
            /*if (checkNow.Count == 0)
            {
                SwapReference(ref checkNow, ref checkNext);
            }*/
        }

        return tilesSearch;
    }

    private int floorDifferenceCost(TileLogic current, TileLogic next)
    {
        if (next == null) return 0;

        int floorDifference = current.floor.height - next.floor.height;

        if (floorDifference < 0) return 1;
        else return 0;
    }

    void ClearSearch()
    {
        foreach (TileLogic t in reachedTiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    public static TileLogic GetTile(Vector3Int pos)
    {
        TileLogic tile = null;
        Board.instance.tiles.TryGetValue(pos, out tile);

        return tile;
    }

}
