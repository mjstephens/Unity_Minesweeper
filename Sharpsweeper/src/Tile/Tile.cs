using System;
using Sharpsweeper.Tile.View;

namespace Sharpsweeper.Tile
{
    /// <summary>
    /// Class representing an instance of a tile.
    /// </summary>
    public class Tile : ITile
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
        public ITile[] neighbors { get; set; }
        public Tuple<int, int> boardPosition { get; set; }
        public int boardIndex { get; set; }

        /// <summary>
        /// The board to which this tile belongs.
        /// </summary>
        private readonly Board.Board _board;

        /// <summary>
        /// "Score" refers to the number of neighbors that contaon bombs.
        /// </summary>
        private int _neighboringBombs;

        #endregion Properties


        #region Construction

        public Tile(Board.Board board)
        {
            _board = board;
        }

        #endregion Construction


        #region Setup

        void ITile.SetAsBomb()
        {
            tileType = TileType.Bomb;
        }
        
        void ITile.SetNeighbors(ITile[] inNeighbors)
        {
            // Count valid neighbors
            neighbors = inNeighbors;
            foreach (ITile tile in neighbors)
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

        void ITile.UpdateTileView()
        {
            view?.SetTileStatus(tileType);
            if (tileType != TileType.Bomb)
            {
                view?.SetNumberOfNeighboringBombs(_neighboringBombs);
            }
        }

        #endregion Setup


        #region Selection

        void ITile.TileSelected()
        {
            // Don't reveal times that are flagged
            if (isFlagged)
                return;
            
            // Reveal the view
            view?.RevealTile();
            isRevealed = true;
            
            // What should we do now that the tile is revealed?
            if (tileType == TileType.Bomb)
            {
                _board?.BombSelected();
            }
            else if (_neighboringBombs <= 0)
            {
                Board.Board.BlankTileSelected(this);
            }
        }

        void ITile.TileFlagged()
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
            _board.OnFlagSet(isFlagged);
        }

        void ITile.ForceReveal()
        {
            isRevealed = true;
            view?.RevealTile();
        }

        #endregion Selection
    }
}