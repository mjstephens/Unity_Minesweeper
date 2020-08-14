using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unitysweeper.Board;
using Unitysweeper.Game;
using Unitysweeper.Serialization;

namespace Unitysweeper.UI.Menu
{
    public class UIDifficultyInstance : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        public TMP_Text difficultyLabel;
        public TMP_Text highScoreLabel;

        /// <summary>
        /// The data associated with this button
        /// </summary>
        private BoardDataTemplate _data;

        #endregion Variables
        
        
        #region Initialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SetData(BoardDataTemplate data)
        {
            _data = data;
            difficultyLabel.text = data.displayLabel;
            
            // Get high score for this option
            int highScore = GameHighScore.GetSavedHighScoreDataForGameDifficulty(data).secondsElapsed;
            if (highScore > 0)
            {
                highScoreLabel.text = "BEST: " + highScore + " seconds";
            }
            else
            {
                highScoreLabel.text = "-";
            }
        }  

        #endregion Initialization


        #region UI Events

        /// <summary>
        /// Called from UI Button
        /// </summary>
        public void OnSelected()
        {
            GameDataTransport.Instance.boardData = _data;
            GameDataTransport.Instance.levelSeed = Random.Range(0, int.MaxValue);
            SceneManager.LoadScene(1);
        }

        #endregion UI Events
    }
}
