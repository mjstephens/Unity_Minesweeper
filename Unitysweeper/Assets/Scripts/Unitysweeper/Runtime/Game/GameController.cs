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
        
        private IGameSystem _gameSystem;
        public Sharpsweeper.Game.Game game { get; private set; }
        private string _difficultyKey;

        #endregion Variables


        #region Initialization

        /// <summary>
        /// 
        /// </summary>
        public void Awake()
        {
            // Create a new game
            game = new Sharpsweeper.Game.Game(
                gameView,
                GameDataTransport.Instance.boardData.data, 
                GameDataTransport.Instance.levelSeed);
            _gameSystem = game;
            
            // Create board
            MonoBoardInstance boardObj = Instantiate(monoBoardObj).GetComponent<MonoBoardInstance>();
            boardObj.viewData = GameDataTransport.Instance.styleData;

            // 
            boardObj.ConstructView(game.board);
            _difficultyKey = GameHighScore.GetDifficultyKey(GameDataTransport.Instance.boardData);
            
            // Move camera to center board
            gameCameraTrans.position = new Vector3(
                game.board.data.xSize / 2f,
                game.board.data.ySize / 2f,
                gameCameraTrans.position.z);

            // Start the game
            ((IGameSystem)game).BeginGame();
        }
        
        #endregion Initialization


        #region Update

        private void Update()
        {
            _gameSystem?.UpdateGame();
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
            if (GameHighScore.ReportGameScoreData(data, _difficultyKey))
            {
                Debug.Log("HIGH SCORE!");
            }

            winCanvas.GetComponent<UIView_GameWinCanvas>().SetEndGameData(data.secondsElapsed);
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
            switch (game.state)
            {
                case Sharpsweeper.Game.Game.GameState.Lose:
                    OnLose();
                    break;
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