using TMPro;
using UnityEngine;
using Unitysweeper.View;

namespace Unitysweeper.UI
{
    public class UIView_FlagCounter : MonoBehaviour, IPlayerFlagsView
    {
        #region Variables

        [Header("References")]
        public TMP_Text flagRemainingLabel;

        #endregion Variables


        #region Bombs

        void IPlayerFlagsView.SetFlagsTotal(int count)
        {
            flagRemainingLabel.text = count.ToString();
        }

        #endregion Bombs
    }
}
