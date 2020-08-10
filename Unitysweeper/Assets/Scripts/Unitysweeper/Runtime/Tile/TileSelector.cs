using UnityEngine;
using Unitysweeper.Game;

namespace Unitysweeper.Tile
{
    /// <summary>
    /// Uses raycasting to determine which tiles the player selects.
    /// </summary>
    public class TileSelector : MonoBehaviour
    {
        #region Variables

        [Header("References")] 
        public Camera sceneCamera;
        public GameController gameController;

        [Header("Settings")] 
        public LayerMask tileMask;

        #endregion Variables
        
        
        #region Input

        private void Update()
        {
            // Guard clause
            if (gameController.game.state != Sharpsweeper.Game.Game.GameState.InProgress)
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
                TileCollisionReceiver selected = RaycastToBoard(Input.mousePosition, sceneCamera, tileMask);
                TileSelected(selected);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                TileCollisionReceiver selected = RaycastToBoard(Input.mousePosition, sceneCamera, tileMask);
                TileFlagged(selected);
            }
        }

        /// <summary>
        /// Raycasts to the game board from the given position.
        /// </summary>
        /// <param name="raySource">The screen position source of the ray.</param>
        /// <param name="cam">The camera through which to pass the ray.</param>
        /// <param name="mask">Valid collision mask.</param>
        private static TileCollisionReceiver RaycastToBoard(Vector3 raySource, Camera cam, LayerMask mask)
        {
            TileCollisionReceiver tileSelected = null;
            Ray ray = cam.ScreenPointToRay(raySource);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, float.MaxValue, mask);
            if (hit.transform != null)
            {
                tileSelected = hit.transform.GetComponent<TileCollisionReceiver>();
            }
            return tileSelected;
        }

        #endregion Input


        #region Selection

        /// <summary>
        /// Called when a tile has been "selected" (uncovered) by the player.
        /// </summary>
        /// <param name="tile">The collision receiver of the tile that was selected.</param>
        private static void TileSelected(TileCollisionReceiver tile)
        {
            if (tile != null)
            {
                tile.OnTileSelected();
            }
        }
        
        /// <summary>
        /// Called when a tile has been "flagged" by the player.
        /// </summary>
        /// <param name="tile">The collision receiver of the tile that was flagged.</param>
        private static void TileFlagged(TileCollisionReceiver tile)
        {
            if (tile != null)
            {
                tile.OnTileFlagged();
            }
        }

        #endregion Selection
    }
}