using System;
using Sharpsweeper.Board;
using Sharpsweeper.Tile.View;

namespace Sharpsweeper.Tile
{
    /// <summary>
    /// Class representing an instance of a tile.
    /// </summary>
    public class Tile : ITileSimulation
    {
        public enum TileType
        {
            Default,
            Bomb,
            Blank
        }
        
        #region Properties

        public ITileView view { get; set; }
        public TileType tileType { get; set; }
        public bool isFlagged { get; private set; }
        public bool isRevealed { get; private set; }
        /// <summary>
        /// Neighbors array organized clockwise starting from top-left tile relative to this tile
        /// index 0 = top-left, 1 = top, 2 = top-right, 3 = right, etc
        /// </summary>
        public ITileSimulation[] neighbors { get; private set; }
        public Tuple<int, int> boardPosition { get; set; }
        public int boardIndex { get; set; }

        /// <summary>
        /// The board to which this tile belongs.
        /// </summary>
        private readonly IBoardSimulation _board;

        /// <summary>
        /// "Score" refers to the number of neighbors that contaon bombs.
        /// </summary>
        private int _neighboringBombs;

        #endregion Properties


        #region Construction

        public Tile(IBoardSimulation board)
        {
            _board = board;
            tileType = TileType.Default;
        }

        #endregion Construction


        #region Setup

        void ITileSimulation.SetAsBomb()
        {
            tileType = TileType.Bomb;
        }
        
        void ITileSimulation.SetNeighbors(ITileSimulation[] inNeighbors)
        {
            // Count valid neighbors
            neighbors = inNeighbors;
            foreach (ITileSimulation tile in neighbors)
            {
                if (tile != null && tile.tileType == TileType.Bomb)
                {
                    _neighboringBombs++;
                }
            }
            
            // If there are no neighboring bombs, this is a "blank" tile
            if (_neighboringBombs == 0 && tileType != TileType.Bomb)
            {
                tileType = TileType.Blank;
            }
        }

        void ITileSimulation.UpdateTileView()
        {
            view?.SetTileStatus(tileType);
            if (tileType != TileType.Bomb)
            {
                view?.SetNumberOfNeighboringBombs(_neighboringBombs);
            }
        }

        #endregion Setup


        #region Selection

        void ITileSimulation.TileSelected()
        {
            // Don't reveal times that are flagged
            if (isFlagged)
                return;
            
            // Reveal the view
            _board.OnTileSelected(this);
            view?.RevealTile();
            isRevealed = true;
        }

        void ITileSimulation.TileFlagged()
        {
            // Can't flag tiles that are already revealed
            if (isRevealed)
                return;

            // We need to make sure we have flags left
            if (!isFlagged)
            {
                if (_board.flagsRemaining <= 0)
                {
                    return;
                }
            }
            
            isFlagged = !isFlagged;
            view?.FlagTile();
            _board.OnTileFlagged(this);
        }

        void ITileSimulation.ForceReveal()
        {
            isRevealed = true;
            view?.RevealTile();
        }

        #endregion Selection
    }
}