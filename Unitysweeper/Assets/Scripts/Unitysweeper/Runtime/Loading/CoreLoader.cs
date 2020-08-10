using UnityEngine;

namespace Unitysweeper.Loading
{
    public class CoreLoader : MonoBehaviour
    {
        #region Variables

        /// <summary>
        /// The name of the data transport object.
        /// </summary>
        private const string CONST_dataObjName = "Core/DataTransport";

        #endregion Variables


        #region Loading

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadCore()
        {
            // Instantiate the core data object
            Instantiate(Resources.Load(CONST_dataObjName));
        }

        #endregion Loading
    }
}
