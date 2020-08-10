using System.Collections;
using TMPro;
using UnityEngine;

namespace Unitysweeper.UI.Menu
{
    /// <summary>
    /// Animates the "Minesweeper" title in the menu.
    /// </summary>
    public class UITitleAnimator : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        public TMP_Text titleText;
        
        [Header("Animation")]
        public float targetCharacterSpacing = 30;
        public AnimationCurve characterSpacingAnimCurve;
        public float animationDuration;

        #endregion Variables


        #region Initialization

        private void OnEnable()
        {
            StartCoroutine(AnimateTitleSpacing());
        }

        #endregion Initialization


        #region Animation

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerator AnimateTitleSpacing()
        {
            titleText.characterSpacing = 0;
            float animationTimeElapsed = 0;
            float timeStarted = Time.time;
            
            // Interpolate character spacing based on animation curve
            while (animationTimeElapsed < animationDuration)
            {
                animationTimeElapsed = Time.time - timeStarted;
                float eval = characterSpacingAnimCurve.Evaluate(animationTimeElapsed / animationDuration);
                titleText.characterSpacing = eval * targetCharacterSpacing;
                yield return null;
            }
        }

        #endregion Animation
    }
}
