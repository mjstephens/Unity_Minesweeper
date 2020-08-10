using UnityEngine;
using Unitysweeper.Board;
using Unitysweeper.Data;

namespace Unitysweeper.Game
{
    /// <summary>
    /// Facilitates data transport between scenes (menu -> game).
    /// </summary>
    public class GameDataTransport : MonoBehaviour
    {
        #region Singleton

        /// <summary>
        /// Singleton ref
        /// </summary>
        public static GameDataTransport Instance;

        #endregion Singleton
        

        #region Data

        [Header("Manifests")]
        [SerializeField]
        private AssetManifest difficultiesManifest;
        [SerializeField]
        private AssetManifest stylesManifest;
        
        /// <summary>
        /// The selected board data
        /// </summary>
        public BoardDataTemplate boardData { get; set; }
        
        /// <summary>
        /// The selected style data
        /// </summary>
        public BoardViewDataTemplate styleData { get; private set; }
        
        /// <summary>
        /// The seed being used for the current level
        /// </summary>
        public int levelSeed { get; set; }

        #endregion Data
        
        
        #region Initialization

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                
                // Set defaults
                boardData = difficultiesManifest.manifest[0] as BoardDataTemplate;
                styleData = stylesManifest.manifest[0] as BoardViewDataTemplate;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion Initialization


        #region Getters

        public Object[] GetDifficulties()
        {
            return difficultiesManifest.manifest.ToArray();
        }
        
        public BoardViewDataTemplate[] GetStyles()
        {
            return stylesManifest.manifest.ToArray() as BoardViewDataTemplate[];
        }

        #endregion Getters
    }
}
