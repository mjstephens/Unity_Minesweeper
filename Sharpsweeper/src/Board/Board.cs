using System;
using Sharpsweeper.Board.Data;
using Sharpsweeper.Game;
using Sharpsweeper.Tile;

namespace Sharpsweeper.Board
{
    /// <summary>
    /// Holds information and logic relating to the minesweeper board (grid of tiles).
    /// </summary>
    public class Board
    {
        #region Properties
        
        /// <summary>
        /// The data used to construct this board.
        /// </summary>
        public BoardData data { get; }
        
        /// <summary>
        /// Array of tiles this board contains
        /// </summary>
        public ITile[,] tiles { get; }

        /// <summary>
        /// The number of flags that the player has remaining.
        /// </summary>
        public int flagsRemaining { get; private set; }
        
        public int totalBombs { get; private set; }
        
        /// <summary>
        /// The game instance.
        /// </summary>
        private readonly Game.Game _game;

        #endregion Properties
        

        #region Constructor

        public Board(
            BoardData inData, 
            Game.Game inGame, 
            int seed)
        {
            // Set data
            data = inData;
            _game = inGame;
            
            // Create board tiles
            tiles = new ITile[data.xSize, data.ySize];
            int runningIndex = 0;
            for (int i = 0; i < data.xSize; i++)
            { 
                for (int e = 0; e < data.ySize; e++)
                {
                    tiles[i, e] = new Tile.Tile(this)
                    {
                        boardPosition = new Tuple<int, int>(i, e),
                        boardIndex = runningIndex
                    };
                    runningIndex++;
                }
            }
            
            // Distribute bombs amongst tiles
            totalBombs = DistributeBombs(data.bombProbability, tiles, seed);
            flagsRemaining = totalBombs;
            
            // Assign "neighbor numbers"
            SetNeighborNumbers(tiles, data.xSize, data.ySize);
            
            // Tiles are finished setting up
            OnBoardSetupComplete(tiles);
        }

        #endregion Constructorå


        #region Setup

        private static int DistributeBombs(float probability, ITile[,] tiles, int seed)
        {
            Random r = new Random(seed);
            int bombCount = 0;
            foreach (ITile tile in tiles)
            {
                if (probability > r.NextDouble())
                {
                    tile.tileType = Tile.Tile.TileType.Bomb;
                    bombCount++;
                }
            }
            return bombCount;
        }

        private static void SetNeighborNumbers(
            ITile[,] tiles,
            int boardWidth, 
            int boardHeight)
        {
            foreach (ITile tile in tiles)
            {
                tile.SetNeighbors(TileNeighborUtility.GetNeighborTiles(tile, tiles, boardWidth, boardHeight));
            }
        }

        private static void OnBoardSetupComplete(ITile[,] tiles)
        {
            foreach (ITile tile in tiles)
            {
                tile.UpdateTileView();
            }
        }
        
        #endregion Setup
        

        #region Tile Selection

        public void BombSelected()
        {
            // All bombs should be revealed
            foreach (ITile t in tiles)
            {
                if (t.tileType == Tile.Tile.TileType.Bomb)
                {
                    t.ForceReveal();
                }
            }
            
            ((IGameSystem)_game).GameLost();
        }

        public static void BlankTileSelected(ITile tile)
        {
            foreach (ITile t in BoardRevealer.GetAdjacentTiles(tile))
            {
                t.ForceReveal();
            }
        }

        #endregion Tile Selection


        #region Flag
        
        public int GetNumberOfFlaggedBombs()
        {
            int flagged = 0;
            foreach (ITile t in tiles)
            {
                if (t.tileType == Tile.Tile.TileType.Bomb && t.isFlagged)
                {
                    flagged++;
                }
            }
            return flagged;
        }

        // When any tile has been flagged (true) or unflagged (false)
        public void OnFlagSet(bool set)
        {
            if (set)
            {
                flagsRemaining--;
                
                // Are we out of flags? If so, check for victory
                if (flagsRemaining == 0)
                {
                    if (CheckVictoryCondition())
                    {
                        ((IGameSystem)_game).GameWon();
                    }
                }
            }
            else
            {
                flagsRemaining++;
            }
            
            // Update flags in game
            _game.currentData.flagsRemaining = flagsRemaining;
        }

        // Returns true only if all bomb tiles are flagged.
        private bool CheckVictoryCondition()
        {
            bool won = true;
            foreach (ITile t in tiles)
            {
                if (t.tileType == Tile.Tile.TileType.Bomb && !t.isFlagged)
                {
                    won = false;
                    break;
                }
            }
            return won;
        }

        #endregion Flag
    }
}