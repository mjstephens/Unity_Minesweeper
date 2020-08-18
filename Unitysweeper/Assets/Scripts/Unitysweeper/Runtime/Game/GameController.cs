using Sharpsweeper.Board;
using Sharpsweeper.Board.Data;
using Sharpsweeper.Game;
using Sharpsweeper.Game.Data;
using UnityEngine;
using Unitysweeper.Board;
using Unitysweeper.Serialization;
using Unitysweeper.UI;
using Unitysweeper.View;

namespace Unitysweeper.Game
{
    public class GameController : MonoBehaviour
    {
        #region Variables

        [Header("References")] 
        public GameView gameView;
        public GameObject monoBoardObj;
        public Transform gameCameraTrans;

        [Header("UI References")] 
        public GameObject winCanvas;
        public GameObject loseCanvas;
        public GameObject gameEndedCanvas;
        
        public GameState state => _game.state;
        private IGameSimulation _game;
        private string _difficultyKey;

        #endregion Variables


        #region Initialization

        private void Awake()
        {
            // Create a new game
            _game = new Sharpsweeper.Game.Game(
                gameView,
                GameDataTransport.Instance.boardData.data, 
                GameDataTransport.Instance.levelSeed);
            
            // Create the board objects
            ConstructBoardObject(_game.board, GameDataTransport.Instance.boardData.data);
            
            // Start the game
            _game.BeginGame();
        }

        private void ConstructBoardObject(IBoardSimulation board, BoardData boardData)
        {
            MonoBoardInstance boardObj = Instantiate(monoBoardObj).GetComponent<MonoBoardInstance>();
            boardObj.viewData = GameDataTransport.Instance.styleData;
            boardObj.ConstructView(board);
            _difficultyKey = GameHighScore.GetDifficultyKey(GameDataTransport.Instance.boardData);
            
            // Move camera to center board
            gameCameraTrans.position = new Vector3(
                boardData.xSize / 2f,
                boardData.ySize / 2f,
                gameCameraTrans.position.z);
        }
        
        #endregion Initialization


        #region Update

        private void Update()
        {
            _game?.UpdateGame();
        }

        #endregion Update


        #region State

        public void OnGameLost(int secondsElapsed, int bombCount)
        {
            loseCanvas.GetComponent<UIView_GameLostCanvas>().SetEndGameData(secondsElapsed, bombCount);
        }
        
        public void OnGameWin(GameSummaryData data)
        {
            // Check for high score
            bool highScore = GameHighScore.ReportGameScoreData(data, _difficultyKey);
            winCanvas.GetComponent<UIView_GameWinCanvas>().SetEndGameData(data.secondsElapsed, highScore);
            OnWin();
        }

        #endregion State


        #region End

        /// <summary>
        /// When the player inputs after the game is over, we can show the end screen.
        /// </summary>
        public void OnGameEndInput()
        {
            // Currently we only wait for input on a loss
            if (_game.state == GameState.Lose)
            {
                OnLose();
            }
        }

        private void OnLose()
        {
            loseCanvas.SetActive(true);
            gameEndedCanvas.SetActive(true);
        }
        
        private void OnWin()
        {
            winCanvas.SetActive(true);
            gameEndedCanvas.SetActive(true);
        }

        #endregion End
    }
}