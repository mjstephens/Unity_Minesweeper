using Sharpsweeper.Game;
using UnityEngine;

namespace Unitysweeper.Game
{
    public class GameEndInput : MonoBehaviour
    {
        #region Variables

        [Header("References")] 
        public GameController gameController;

        /// <summary>
        /// 
        /// </summary>
        private bool _canEndGame;

        #endregion Variables


        #region Update

        private void Update()
        {
            // Guard clause
            if (gameController.state == GameState.InProgress ||
                gameController.state == GameState.Waiting)
                return;
        
            // Check for end game input
            if (Input.GetMouseButtonDown(0) && _canEndGame)
            {
                gameController.OnGameEndInput();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _canEndGame = true;
            }
        }

        #endregion Update
    }
}
