using System.Collections.Generic;
using Sharpsweeper.Tile;

namespace Sharpsweeper.Board
{
    /// <summary>
    /// Helps with revealing adjacent tiles across the board.
    /// </summary>
    public static class BoardRevealer
    {
        #region Variables

        /// <summary>
        /// Keeps track of checked tiles to prevent infinite recursion.
        /// </summary>
        private static List<ITileSimulation> checkedTiles = new List<ITileSimulation>();
        
        /// <summary>
        /// A running list of tiles to be revealed.
        /// </summary>
        private static List<ITileSimulation> revealTiles = new List<ITileSimulation>();

        #endregion Variables


        #region Reveal

        public static List<ITileSimulation> GetAdjacentTiles(ITileSimulation sourceTile)
        {
            checkedTiles = new List<ITileSimulation>{sourceTile};
            revealTiles = new List<ITileSimulation>{sourceTile};
            CollectAdjacentBlankTiles(sourceTile);
            revealTiles = GetBorderTiles(revealTiles);

            return revealTiles;
        }
        
        private static void CollectAdjacentBlankTiles(ITileSimulation sourceTile)
        {
            checkedTiles.Add(sourceTile);
            foreach (ITileSimulation tile in sourceTile.neighbors)
            {
                if (tile != null && 
                    tile.tileType == Tile.Tile.TileType.Blank)
                {
                    if (!checkedTiles.Contains(tile))
                    {
                        CollectAdjacentBlankTiles(tile);
                        revealTiles.Add(tile);
                    }
                }
            }
        }
        
        private static List<ITileSimulation> GetBorderTiles(List<ITileSimulation> tiles)
        {
            List<ITileSimulation> borderTiles = new List<ITileSimulation>();
            foreach (ITileSimulation tile in tiles)
            {
                foreach (ITileSimulation n in tile.neighbors)
                {
                    if (n != null && n.tileType != Tile.Tile.TileType.Bomb)
                    {
                        borderTiles.Add(n);
                    }
                }
            }
            tiles.AddRange(borderTiles);
            return tiles;
        }

        #endregion Reveal
    }
}