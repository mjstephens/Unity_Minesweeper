using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unitysweeper.Game;
using Random = UnityEngine.Random;

namespace Unitysweeper.UI
{
    public class UIView_GameWinCanvas : MonoBehaviour
    {
        #region References

        [Header("References")]
        public TMP_Text timeElapsedText;
        public GameObject highScoreObj;

        #endregion References


        #region Data

        public void SetEndGameData(int secondsElapsed, bool highScore)
        {
            timeElapsedText.text = secondsElapsed.ToString("F0");
            highScoreObj.SetActive(highScore);
        }

        #endregion Data


        #region UI Events

        /// <summary>
        /// From the "Play again" button; reloads the scene
        /// </summary>
        public void OnUIPlayAgainSelected()
        {
            GameDataTransport.Instance.levelSeed = Random.Range(0, int.MaxValue);
            SceneManager.LoadScene(1);
        }

        #endregion UI Events
    }
}
