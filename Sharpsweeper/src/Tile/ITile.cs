using System;
using Sharpsweeper.Tile.View;

namespace Sharpsweeper.Tile
{
    public interface ITile
    {
        #region Properties

        ITileView view { get; set; }
        Tile.TileType tileType { get; set; }
        bool isFlagged { get; }
        ITile[] neighbors { get; }
        Tuple<int, int> boardPosition { get; set; }
        int boardIndex { get; set; }

        #endregion Properties
        
        void SetAsBomb();
        
        void SetNeighbors(ITile[] neighbors);

        void UpdateTileView();
        
        void TileSelected();
        
        void TileFlagged();

        void ForceReveal();
    }
}