using System.Diagnostics;

namespace Sharpsweeper.Game
{
    /// <summary>
    /// Keeps track of elapsed game time.
    /// </summary>
    public class GameTime : IGameTimeSource
    {
        #region Variables

        private readonly Game _game;
        
        public readonly Stopwatch gameElapsed = new Stopwatch();

        #endregion Variables
        
        
        #region Construction

        public GameTime(Game game)
        {
            _game = game;
        }

        #endregion Construction


        #region State

        void IGameTimeSource.BeginGameTimer()
        {
            if (!gameElapsed.IsRunning)
            {
                gameElapsed.Start();
            }
        }
        
        void IGameTimeSource.EndGameTimer()
        {
            gameElapsed.Stop();
        }

        #endregion State
        
        
        #region Time

        void IGameTimeSource.UpdateGameTimeElapsed()
        {
            _game.currentData.timeElapsed = gameElapsed.Elapsed;
        }
        
        #endregion Time
    }
}