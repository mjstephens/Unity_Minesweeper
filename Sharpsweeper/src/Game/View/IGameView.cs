using Sharpsweeper.Game.Data;

namespace Sharpsweeper.Game.View
{
    /// <summary>
    /// Implement this interface client-side to respond to game updates.
    /// </summary>
    public interface IGameView
    {
        void GameSet(GameConfigurationData data);
        void UpdateGame(GameProgressData data);
        void OnGameFinished(GameSummaryData data);
    }
}