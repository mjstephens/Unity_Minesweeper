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
        private static List<ITile> checkedTiles = new List<ITile>();
        
        /// <summary>
        /// A running list of tiles to be revealed.
        /// </summary>
        private static List<ITile> revealTiles = new List<ITile>();

        #endregion Variables


        #region Reveal

        public static List<ITile> GetAdjacentTiles(ITile sourceTile)
        {
            checkedTiles = new List<ITile>{sourceTile};
            revealTiles = new List<ITile>{sourceTile};
            CollectAdjacentBlankTiles(sourceTile);
            revealTiles = GetBorderTiles(revealTiles);

            return revealTiles;
        }
        
        private static void CollectAdjacentBlankTiles(ITile sourceTile)
        {
            checkedTiles.Add(sourceTile);
            foreach (ITile tile in sourceTile.neighbors)
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
        
        private static List<ITile> GetBorderTiles(List<ITile> tiles)
        {
            List<ITile> borderTiles = new List<ITile>();
            foreach (ITile tile in tiles)
            {
                foreach (ITile n in tile.neighbors)
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