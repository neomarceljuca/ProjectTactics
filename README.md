# Project Tactics
### Trabalho para a disciplina de grafos

O projeto se baseia em um jogo rpg por turnos estilo tático, onde cada jogador tem a habilidade de executar ações durante seu turno. 

O cenário do jogo é feito de blocos ou  **_tiles_**, montando assim uma espécie de mapa com as posições relativas à dimensão de cada bloco (x,y,z). 

## Representação com Grafos e seus Algoritmos
Cada bloco do mapa possui um objeto _TileLogic_ que registra as informações pertinentes, incluindo sua posição (x,y,z) física e relativa a estrutura de _TileMap_ do mapa. 

O projeto utiliza algoritmo de Dijkstra para deixar registrado em cada objeto o  seu predecessor de modo que seja possível montar o caminho mínimo a partir do bloco de origem do personagem sempre que este for se movimentar durante o jogo. 

## Classes Relevantes 

- Board
- Movement
- TileLogic
- MovementSelection (State)
