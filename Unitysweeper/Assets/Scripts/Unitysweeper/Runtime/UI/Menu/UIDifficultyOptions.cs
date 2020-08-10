using UnityEngine;
using Unitysweeper.Board;
using Unitysweeper.Game;

namespace Unitysweeper.UI.Menu
{
    /// <summary>
    /// Populates the menu list of difficulty options.
    /// </summary>
    public class UIDifficultyOptions : MonoBehaviour
    {
        #region Variables

        [Header("Resources")]
        public GameObject difficultySelectionButtonPrefab;
        
        [Header("References")]
        public Transform uiDifficultyButtonParent;

        #endregion Variables


        #region Difficulty Selection

        private void Start()
        {
            // Get difficulties manifest
            foreach (var d in GameDataTransport.Instance.GetDifficulties())
            {
                GameObject g = Instantiate(difficultySelectionButtonPrefab, uiDifficultyButtonParent);
                g.GetComponent<UIDifficultyInstance>().SetData(d as BoardDataTemplate);
            }
        }

        #endregion Difficulty Selection
    }
}
