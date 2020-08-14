using System;
using Sharpsweeper.Board.Data;
using Sharpsweeper.Game;
using Sharpsweeper.Tile;

namespace Sharpsweeper.Board
{
    /// <summary>
    /// Holds information and logic relating to the minesweeper board (grid of tiles).
    /// </summary>
    public class Board : IBoardSimulation
    {
        #region Properties
        
        /// <summary>
        /// Array of tiles this board contains
        /// </summary>
        public ITileSimulation[,] tiles { get; private set; }
        public Tuple<int, int> boardSize { get; private set; }
        public int flagsRemaining { get; private set; }
        public int flaggedBombs => GetNumberOfFlaggedBombs(tiles);
        public int totalBombs { get; }
        
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
            boardSize = new Tuple<int, int>(inData.xSize, inData.ySize);
            _game = inGame;
            
            // Create board tiles
            tiles = new ITileSimulation[inData.xSize, inData.ySize];
            int runningIndex = 0;
            for (int i = 0; i < inData.xSize; i++)
            { 
                for (int e = 0; e < inData.ySize; e++)
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
            totalBombs = DistributeBombs(inData.bombProbability, tiles, seed);
            flagsRemaining = totalBombs;
            
            // Assign "neighbor numbers"
            SetNeighborNumbers(tiles, inData.xSize, inData.ySize);
            
            // Tiles are finished setting up
            OnBoardSetupComplete(tiles);
        }

        #endregion Constructorå


        #region Setup

        private static int DistributeBombs(float probability, ITileSimulation[,] tiles, int seed)
        {
            Random r = new Random(seed);
            int bombCount = 0;
            foreach (ITileSimulation tile in tiles)
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
            ITileSimulation[,] tiles,
            int boardWidth, 
            int boardHeight)
        {
            foreach (ITileSimulation tile in tiles)
            {
                tile.SetNeighbors(TileNeighborUtility.GetNeighborTiles(tile, tiles, boardWidth, boardHeight));
            }
        }

        private static void OnBoardSetupComplete(ITileSimulation[,] tiles)
        {
            foreach (ITileSimulation tile in tiles)
            {
                tile.UpdateTileView();
            }
        }
        
        #endregion Setup
        

        #region Tile Selection

        void IBoardSimulation.OnTileSelected(ITileSimulation tile)
        {
            // Bump game simulation
            _game.OnGameInput();
            
            // What was the tile type we selected?
            if (tile.tileType == Tile.Tile.TileType.Bomb)
            {
                RevealAllBombs(tiles);
                ((IGameSimulation)_game).GameLost();
            }
            else if (tile.tileType == Tile.Tile.TileType.Blank)
            {
                RevealAdjacentBlankTiles(tile);
            }
        }
        
        void IBoardSimulation.OnTileFlagged(ITileSimulation tile)
        {
            // Bump game simulation
            _game.OnGameInput();
            
            if (tile.isFlagged)
            {
                flagsRemaining--;
                
                // Are we out of flags? If so, check for victory
                if (flagsRemaining == 0)
                {
                    if (Game.Game.CheckVictoryConditions(this))
                    {
                        ((IGameSimulation)_game).GameWon();
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

        private static void RevealAllBombs(ITileSimulation[,] tiles)
        {
            foreach (ITileSimulation t in tiles)
            {
                if (t.tileType == Tile.Tile.TileType.Bomb)
                {
                    t.ForceReveal();
                }
            }
        }

        private static void RevealAdjacentBlankTiles(ITileSimulation tile)
        {
            foreach (ITileSimulation t in BoardRevealer.GetAdjacentTiles(tile))
            {
                t.ForceReveal();
            }
        }

        #endregion Tile Selection


        #region Flag
        
        private static int GetNumberOfFlaggedBombs(ITileSimulation[,] tiles)
        {
            int flagged = 0;
            foreach (ITileSimulation t in tiles)
            {
                if (t.tileType == Tile.Tile.TileType.Bomb && t.isFlagged)
                {
                    flagged++;
                }
            }
            return flagged;
        }

        #endregion Flag
    }
}