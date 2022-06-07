using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetState : State, ISearcher
{
    public Dictionary<Vector3Int, TileLogic> tiles;
    List<TileLogic> reachedTiles;

    public List<Floor> floors;
    [HideInInspector]
    public Vector3Int[] dirs = new Vector3Int[4] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };


    public override void Enter()
    {
        base.Enter();
        inputs.OnMove += OnMoveTileSelector;
        inputs.OnFire += OnFire;

        tiles = Board.instance.tiles;
        reachedTiles = Search(Turn.unit.tile);
        //if(!Turn.skill.canTargetSelf) reachedTiles.Remove(Turn.unit.tile);
        Board.instance.SelectTiles(reachedTiles, Turn.unit.alliance);
        State.lookToTile += displayStatsMenu;

        if(reachedTiles.Contains(machine.selectedTile)) machine.characterStatsPanel.MoveTo("Show");

    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove -= OnMoveTileSelector;
        inputs.OnFire -= OnFire;
        State.lookToTile -= displayStatsMenu;
        Board.instance.DeselectTiles(reachedTiles);

        machine.myStatDisplayer.UpdateUI(Turn.unit);
        machine.characterStatsPanel.MoveTo("Hide");
    }

    void OnFire(object sender, object args)
    {
        int button = (int)args;


        if (button == 1)
        {
            //checar se eh valido
            //futuramente substituir por implementação da validação inteiramente dentro de Turn.skill.ValidadeTarget()
            if (Turn.skill.ValidadeTarget() && reachedTiles.Contains(machine.selectedTile) )
                machine.ChangeTo<PerformSkillState>();

        }
        else if (button == 2)
        {
            machine.ChangeTo<SkillSelectionState>();

        }
    }


    //Implementar personalizadamente para um ataque
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
        if(Turn.skill.canTargetSelf) tilesSearch.Add(start);

        //dist[v] = infinity, prev[v] = undefined
        ClearSearch();

        // Q = todos os nodos do grafo
        //// TO DO: Implementar Min Heap em outra classe para substituir checkNext e checkNow por uma só priority Queue
        //Queue<TileLogic> checkNext = new Queue<TileLogic>();
        //Queue<TileLogic> checkNow = new Queue<TileLogic>();

        List<TileLogic> ListaPrioritaria = new List<TileLogic>();
        //ListaPrioritaria.Add(start);
        foreach (TileLogic t in tiles.Values)
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

                requiredCost = t.distance + 1 + floorDifferenceCost(t, next);

                //nao adcionar se
                if (next == null || next.distance <= requiredCost || requiredCost > Turn.skill.reach)
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
        foreach (TileLogic t in tiles.Values)
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

    void displayStatsMenu(TileLogic t)
    {
        machine.characterStatsPanel.MoveTo("Hide");
        if (t.content != null)
        {
            var mycontent = t.content.GetComponent<Unit>();
            if (mycontent is Unit && reachedTiles.Contains(machine.selectedTile))
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
