using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unitysweeper.UI
{
    public class UINewGameButton : MonoBehaviour
    {
        #region UI Events

        public void OnNewGameButtonSelected()
        {
            SceneManager.LoadScene(0);
        }

        #endregion UI Events
    }
}
