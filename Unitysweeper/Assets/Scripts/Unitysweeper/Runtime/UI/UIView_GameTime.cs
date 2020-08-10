using System;
using TMPro;
using UnityEngine;
using Unitysweeper.View;

namespace Unitysweeper.UI
{
    public class UIView_GameTime : MonoBehaviour, IGameTimeView
    {
        #region Variables

        [Header("References")]
        public TMP_Text gameTimeText;

        #endregion Variables
    
    
        #region Time
        
        public void UpdateGameTime(int secondsElapsed)
        {
            gameTimeText.text = secondsElapsed.ToString("F0");
        }

        #endregion Time
    }
}
