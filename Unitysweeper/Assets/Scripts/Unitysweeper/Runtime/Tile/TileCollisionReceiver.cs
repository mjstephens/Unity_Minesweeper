using UnityEngine;

namespace Unitysweeper.Tile
{
    public class TileCollisionReceiver : MonoBehaviour
    {
        #region Variables

        public MonoTileInstance tile;

        #endregion Variables


        #region Selected

        /// <summary>
        /// Receives collision message and forwards to tile instance.
        /// </summary>
        public void OnTileSelected()
        {
            tile.OnTileSelected();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnTileFlagged()
        {
            tile.OnTileFlagged();
        }

        #endregion Selected
    }
}