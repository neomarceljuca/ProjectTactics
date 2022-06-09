# Project Tactics
### Trabalho para a disciplina de grafos

O projeto se baseia em um jogo rpg por turnos estilo tático, onde cada jogador tem a habilidade de executar ações durante seu turno. 

## Classes Relevantes 

- Board
- TileLogic
- Movement
- MoveSelectionState (Dijkstra)
- SkillTargetState (Dijkstra)

## Bibliotecas/Assets Relevantes 

- Tilemap
- LeanTween



O cenário do jogo é feito de blocos ou  **_tiles_**, montando assim uma espécie de mapa com as posições relativas à dimensão de cada bloco (x,y,z). 





## Representação com Grafos e seus Algoritmos
Cada bloco do mapa possui um objeto _TileLogic_ que registra as informações pertinentes, incluindo sua posição (x,y,z) física e relativa a estrutura de _TileMap_ do mapa. 

![Tiles](https://i.ibb.co/fttbW36/Screenshot-2022-06-09-180033.png)


O projeto utiliza algoritmo de Dijkstra para deixar registrado em cada objeto o  seu predecessor de modo que seja possível montar o caminho mínimo a partir do bloco de origem do personagem sempre que este for se movimentar durante o jogo. 

Toda vez que a máquina estados entrar no estado _MoveSelectionState_ ou _SkillTargetState_, o mapa será interpretado em sua forma de grafo de acordo com suas condições e as distâncias para cada uma das células dentro do alcance do personagens serão calculadas. Além disso, o algoritmo ainda deixa registrado o predecessor de cada bloco para o menor caminho.

Exemplo de mapa ilustrado com valores a serem explorados para distância de alcance =  5
![Exemplo de Mapa com Grafos](https://i.ibb.co/Zd1nLXC/Grafos-Mapa1.png)


## Fim