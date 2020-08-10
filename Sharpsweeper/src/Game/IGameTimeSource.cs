namespace Sharpsweeper.Game
{
    public interface IGameTimeSource
    {
        void BeginGameTimer();
        void UpdateGameTimeElapsed();
        void EndGameTimer();
    }
}