using Sharpsweeper.Tile;
using TMPro;
using UnityEngine;

namespace Unitysweeper.Tile
{
    public class MonoTileInstance : MonoBehaviour, ITileView
    {
        #region Variables

        [Header("References")] 
        public GameObject tileCover;
        public GameObject bombObj;
        public GameObject blankObj;
        public GameObject flagObj;
        public GameObject scoreObj;
        public TMP_Text labelText;
        
        /// <summary>
        /// The tile object that this view represents
        /// </summary>
        public ITile tileObj { get; set; }
 
        #endregion Variables
        
        
        #region Tile

        public void SetTileStatus(Sharpsweeper.Tile.Tile.TileType status)
        {
            bombObj.SetActive(false);
            blankObj.SetActive(false);            
            scoreObj.SetActive(false);

            //
            switch (status)
            {
                case Sharpsweeper.Tile.Tile.TileType.Bomb:
                    bombObj.SetActive(true);
                    break;
                case Sharpsweeper.Tile.Tile.TileType.Blank:
                    blankObj.SetActive(true);
                    break;
                case Sharpsweeper.Tile.Tile.TileType.Default:
                    default:
                    scoreObj.SetActive(true);
                    break;
            }
        }

        void ITileView.SetNumberOfNeighboringBombs(int score)
        {
            if (score > 0)
            {
                labelText.text = score.ToString();
            }
            else
            {
                labelText.text = "";
            }
        }

        void ITileView.RevealTile()
        {
            tileCover.SetActive(false);
        }
        
        void ITileView.FlagTile()
        {
            flagObj.SetActive(!flagObj.activeSelf);
        }

        #endregion Tile


        #region Selection

        public void OnTileSelected()
        {
            tileObj?.TileSelected();
        }

        public void OnTileFlagged()
        {
            tileObj?.TileFlagged();
        }

        #endregion Selection
    }
}