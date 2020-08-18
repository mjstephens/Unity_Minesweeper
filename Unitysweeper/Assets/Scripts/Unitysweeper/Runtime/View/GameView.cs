using Sharpsweeper.Game.Data;
using Sharpsweeper.Game.View;
using UnityEngine;
using Unitysweeper.Game;
using Unitysweeper.UI;

namespace Unitysweeper.View
{
    public class GameView : MonoBehaviour, IGameView
    {
        #region References

        [Header("References")]
        public GameController gameController;
        public UIView_GameTime gameTimeView;
        public UIView_FlagCounter playerFlagsView;

        #endregion References
        
        
        #region View

        void IGameView.OnGameSet(GameConfigurationData data)
        {
            (playerFlagsView as IPlayerFlagsView).SetFlagsTotal(data.totalBombs);
        }

        void IGameView.OnGameUpdated(GameProgressData data)
        {
            (gameTimeView as IGameTimeView).UpdateGameTime((int)data.timeElapsed.TotalSeconds);
            (playerFlagsView as IPlayerFlagsView).SetFlagsTotal(data.flagsRemaining);
        }

        void IGameView.OnGameFinished(GameSummaryData data)
        {
            if (data.didWin)
            {
                gameController.OnGameWin(data);
            }
            else
            {
                gameController.OnGameLost(data.secondsElapsed, data.bombsFlagged);
            }
        }

        #endregion View
    }
}