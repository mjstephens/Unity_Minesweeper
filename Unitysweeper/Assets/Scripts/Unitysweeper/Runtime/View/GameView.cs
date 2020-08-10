using Sharpsweeper.Game.Data;
using Sharpsweeper.View;
using UnityEngine;
using Unitysweeper.Game;
using Unitysweeper.UI;

namespace Unitysweeper.View
{
    public class GameView : MonoBehaviour, ISharpsweeperView
    {
        #region References

        [Header("References")]
        public GameController gameController;
        public UIView_GameTime gameTimeView;
        public UIView_FlagCounter playerFlagsView;

        #endregion References
        
        
        #region View

        void ISharpsweeperView.GameSet(GameConfigurationData data)
        {
            (playerFlagsView as IPlayerFlagsView).SetFlagsTotal(data.totalBombs);
        }

        void ISharpsweeperView.UpdateGame(GameProgressData data)
        {
            (gameTimeView as IGameTimeView).UpdateGameTime((int)data.timeElapsed.TotalSeconds);
            (playerFlagsView as IPlayerFlagsView).SetFlagsTotal(data.flagsRemaining);
        }

        void ISharpsweeperView.OnGameFinished(GameSummaryData data)
        {
            if (data.didWin)
            {
                gameController.OnGameWin(data.secondsElapsed);
            }
            else
            {
                gameController.OnGameLost(data.secondsElapsed, data.bombsFlagged);
            }
        }

        #endregion View
    }
}