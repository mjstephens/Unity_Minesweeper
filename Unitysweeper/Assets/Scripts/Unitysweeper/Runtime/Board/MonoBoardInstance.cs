using Sharpsweeper.Tile;
using UnityEngine;
using Unitysweeper.Tile;

namespace Unitysweeper.Board
{
    public class MonoBoardInstance : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        public GameObject boardSquareInstancePrefab;

        /// <summary>
        /// 
        /// </summary>
        public BoardViewDataTemplate viewData { set; private get; }

        #endregion Variables
        
        
        #region Board

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        public void ConstructView(Sharpsweeper.Board.Board board)
        {
            // Setup tile views
            foreach (ITile tile in board.tiles)
            {
                ConstructBoardSquare(
                    tile.boardPosition.Item1, 
                    tile.boardPosition.Item2,
                    viewData.tileSpacing, 
                    tile);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="tileSpacing"></param>
        /// <param name="instanceData"></param>
        private void ConstructBoardSquare(
            int xPos, 
            int yPos, 
            float tileSpacing, 
            ITile instanceData)
        {
            Vector3 spawnPos = new Vector3(
                xPos * tileSpacing,
                yPos * tileSpacing,
                0);
            MonoTileInstance thisSquare = Instantiate(
                boardSquareInstancePrefab,
                spawnPos,
                Quaternion.identity).GetComponent<MonoTileInstance>();
            
            // Setup view with data
            instanceData.view = thisSquare;
            thisSquare.tileObj = instanceData;
            instanceData.UpdateTileView();
        }

        #endregion Board
    }
}