namespace Sharpsweeper.Tile.View
{
    public interface ITileView
    {
        void SetTileStatus(Tile.TileType status);
        
        void SetNumberOfNeighboringBombs(int score);
        
        void RevealTile();
        
        void FlagTile();
    }
}