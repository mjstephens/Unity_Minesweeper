using System;
using Sharpsweeper.Tile.View;

namespace Sharpsweeper.Tile
{
    public interface ITileSimulation
    {
        #region Properties

        ITileView view { get; set; }
        Tile.TileType tileType { get; set; }
        bool isFlagged { get; }
        bool isRevealed { get; }
        ITileSimulation[] neighbors { get; }
        Tuple<int, int> boardPosition { get; set; }
        int boardIndex { get; set; }

        #endregion Properties
        
        // External - called from client
        void SetAsBomb();
        void SetNeighbors(ITileSimulation[] neighbors);
        void UpdateTileView();
        // External - called from client
        void TileSelected();
        // External - called from client
        void TileFlagged();
        void ForceReveal();
    }
}