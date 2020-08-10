using Sharpsweeper.Tile;
using Sharpsweeper.Tile.View;
using TMPro;
using UnityEngine;

namespace Unitysweeper.Tile
{
    public class MonoTileInstance : MonoBehaviour, ITileView
    {
        #region Variables

        [Header("References")] 
        public GameObject bombObj;
        public GameObject blankObj;
        public GameObject flagObj;
        public GameObject scoreObj;
        public TMP_Text labelText;
        
        private GameObject _activeTileObj;
        public ITile tileObj { get; set; }
 
        #endregion Variables
        
        
        #region Tile

        void ITileView.SetTileStatus(Sharpsweeper.Tile.Tile.TileType status)
        {
            _activeTileObj = null;
            switch (status)
            {
                case Sharpsweeper.Tile.Tile.TileType.Bomb:
                    _activeTileObj = bombObj;
                    break;
                case Sharpsweeper.Tile.Tile.TileType.Blank:
                    _activeTileObj = blankObj;
                    break;
                case Sharpsweeper.Tile.Tile.TileType.Default:
                    default:
                    _activeTileObj = scoreObj;
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
            _activeTileObj.SetActive(true);
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