namespace Sharpsweeper.Game
{
    public interface IGameSystem
    {
        /// <summary>
        /// Tells the game to update/send update to view.
        /// </summary>
        void UpdateGame();
        
        void BeginGame();
        void GameLost();
        void GameWon();
    }
}