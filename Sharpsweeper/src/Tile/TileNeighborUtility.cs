namespace Sharpsweeper.Tile
{
    public static class TileNeighborUtility
    {
        #region MyRegion

        public static ITileSimulation[] GetNeighborTiles(
            ITileSimulation sourceTile, 
            ITileSimulation[,] board,
            int boardWidth, 
            int boardHeight)
        {
            ITileSimulation[] neighbors = new ITileSimulation[8];
            neighbors[0] = GetTileForIndex(GetNWNeighborIndex(sourceTile.boardIndex, boardHeight), board);
            neighbors[1] = GetTileForIndex(GetNNeighborIndex(sourceTile.boardIndex, boardHeight), board);
            neighbors[2] = GetTileForIndex(GetNENeighborIndex(sourceTile.boardIndex, boardWidth, boardHeight), board);
            neighbors[3] = GetTileForIndex(GetENeighborIndex(sourceTile.boardIndex, boardWidth, boardHeight), board);
            neighbors[4] = GetTileForIndex(GetSENeighborIndex(sourceTile.boardIndex, boardWidth, boardHeight), board);
            neighbors[5] = GetTileForIndex(GetSNeighborIndex(sourceTile.boardIndex, boardHeight), board);
            neighbors[6] = GetTileForIndex(GetSWNeighborIndex(sourceTile.boardIndex, boardHeight), board);
            neighbors[7] = GetTileForIndex(GetWNeighborIndex(sourceTile.boardIndex, boardHeight), board);

            return neighbors;
        }

        #endregion
        
        
        #region Neighbors

        private static ITileSimulation GetTileForIndex(int index, ITileSimulation[,] boardTiles)
        {
            if (index == -1)
                return null;

            ITileSimulation instance = null;
            foreach (var square in boardTiles)
            {
                if (square.boardIndex == index)
                {
                    instance = square;
                    break;
                }
            }
            return instance;
        }

        private static int GetNWNeighborIndex(
            int sourceTileIndex, 
            int boardHeight)
        {
            // Are we on the far left or on the top?
            if (GetIsLeftBound(sourceTileIndex, boardHeight) || GetIsTopBound(sourceTileIndex, boardHeight))
                return -1;
            
            // Otherwise return top-left tile
            return (sourceTileIndex - boardHeight) + 1;
        }
        
        private static int GetNNeighborIndex(
            int sourceTileIndex, 
            int boardHeight)
        {
            // Are we on the top?
            if (GetIsTopBound(sourceTileIndex, boardHeight))
                return -1;
            
            // Otherwise return top tile
            return sourceTileIndex + 1;
        }
        
        private static int GetNENeighborIndex(
            int sourceTileIndex,
            int boardWidth,
            int boardHeight)
        {
            // Are we on the far right or on the top?
            if (GetIsRightBound(sourceTileIndex, boardWidth, boardHeight) || GetIsTopBound(sourceTileIndex, boardHeight))
                return -1;
            
            // Otherwise return top-left tile
            return (sourceTileIndex + boardHeight) + 1;
        }
        
        private static int GetENeighborIndex(
            int sourceTileIndex,
            int boardWidth,
            int boardHeight)
        {
            // Are we on the right?
            if (GetIsRightBound(sourceTileIndex, boardWidth, boardHeight))
                return -1;
            
            // Otherwise return right tile
            return sourceTileIndex + boardHeight;
        }
        
        private static int GetSENeighborIndex(
            int sourceTileIndex,
            int boardWidth,
            int boardHeight)
        {
            // Are we on the far right or on the bottom?
            if (GetIsRightBound(sourceTileIndex, boardWidth, boardHeight) || GetIsBottomBound(sourceTileIndex, boardHeight))
                return -1;
            
            // Otherwise return bottom-right tile
            return (sourceTileIndex + boardHeight) - 1;
        }
        
        private static int GetSNeighborIndex(
            int sourceTileIndex, 
            int boardHeight)
        {
            // Are we on the top?
            if (GetIsBottomBound(sourceTileIndex, boardHeight))
                return -1;
            
            // Otherwise return bottom tile
            return sourceTileIndex - 1;
        }
        
        private static int GetSWNeighborIndex(
            int sourceTileIndex,
            int boardHeight)
        {
            // Are we on the far right or on the top?
            if (GetIsLeftBound(sourceTileIndex, boardHeight) || GetIsBottomBound(sourceTileIndex, boardHeight))
                return -1;
            
            // Otherwise return bottom-left tile
            return (sourceTileIndex - boardHeight) - 1;
        }
        
        private static int GetWNeighborIndex(
            int sourceTileIndex,
            int boardHeight)
        {
            // Are we on the top?
            if (GetIsLeftBound(sourceTileIndex, boardHeight))
                return -1;
            
            // Otherwise return left tile
            return sourceTileIndex - boardHeight;
        }

        #endregion Neighbors


        #region Boundaries

        private static bool GetIsLeftBound(int sourceIndex, int boardHeight)
        {
            return sourceIndex <= boardHeight - 1;
        }
        
        private static bool GetIsTopBound(int sourceIndex, int boardHeight)
        {
            return (sourceIndex + 1) % boardHeight == 0;
        }
        
        private static bool GetIsRightBound(int sourceIndex, int boardWidth, int boardHeight)
        {
            return sourceIndex >= (boardWidth * boardHeight) - boardHeight;
        }
        
        private static bool GetIsBottomBound(int sourceIndex, int boardHeight)
        {
            return sourceIndex % boardHeight == 0;
        }
 
        #endregion Boundaries
    }
}