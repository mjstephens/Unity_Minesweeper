using System;
using Sharpsweeper.Tile;

namespace Sharpsweeper.Board
{
    public interface IBoardSimulation
    {
        #region Properties

        ITileSimulation[,] tiles { get; }
        Tuple<int, int> boardSize { get; }
        
        int flagsRemaining { get; }
        int flaggedBombs { get; }
        int totalBombs { get; }

        #endregion Properties

        #region Methods
        
        void OnTileSelected(ITileSimulation tile);
        void OnTileFlagged(ITileSimulation tile);
        
        #endregion
    }
}