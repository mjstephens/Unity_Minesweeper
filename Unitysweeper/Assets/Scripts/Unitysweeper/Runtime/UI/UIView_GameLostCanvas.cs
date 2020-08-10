using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unitysweeper.Game;
using Random = UnityEngine.Random;

namespace Unitysweeper.UI
{
    public class UIView_GameLostCanvas : MonoBehaviour
    {
        #region References

        [Header("References")]
        public TMP_Text timeElapsedText;
        public TMP_Text bombsUncoveredText;

        #endregion References


        #region Data

        public void SetEndGameData(int secondsElapsed, int bombsUncovered)
        {
            timeElapsedText.text = secondsElapsed.ToString("F0");
            bombsUncoveredText.text = bombsUncovered.ToString();
        }

        #endregion Data
        
        
        #region UI Events

        /// <summary>
        /// From the "Play again" button; reloads the scene
        /// </summary>
        public void OnUITryAnother()
        {
            GameDataTransport.Instance.levelSeed = Random.Range(0, int.MaxValue);
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnUITrySameLevel()
        {
            SceneManager.LoadScene(1);
        }

        #endregion UI Events
    }
}
