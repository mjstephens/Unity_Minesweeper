using System;
using Sharpsweeper.Board.Data;
using Sharpsweeper.Game.Data;
using Sharpsweeper.Game.View;

namespace Sharpsweeper.Game
{
    public class Game : IGameSystem
    {
        #region Properties

        public enum GameState
        {
            Waiting,
            InProgress,
            Lose,
            Win
        }
        public GameState state { get; private set; }

        /// <summary>
        /// The time component
        /// </summary>
        public IGameTimeSource time { get; }

        /// <summary>
        /// The board for this game
        /// </summary>
        public Board.Board board { get; }

        /// <summary>
        /// The view to receive state information
        /// </summary>
        private readonly IGameView _view;

        #endregion Properties


        #region Data

        public GameConfigurationData configData { get; private set; }
        public GameProgressData currentData { get; private set; }

        #endregion Data
        
        
        #region Construction
        
        /*
         *    Requires client to create game instance with view data (interfaces) and
         *    board data (difficulty), then call BeginGame() to begin.
         */

        public Game(
            IGameView view, 
            BoardData boardData, 
            int boardSeed)
        {
            // Constuct a new board with the game data
            board = new Board.Board(
                boardData, 
                this, 
                boardSeed);
            
            // Setup data
            configData = GetGameConfigurationData(boardData);

            // Create the game time component
            time = new GameTime(this);
            
            // Set state view
            _view = view;
            _view.GameSet(configData);
        }

        GameConfigurationData GetGameConfigurationData(BoardData bd)
        {
            return new GameConfigurationData
            {
                boardData = bd,
                totalBombs = board.totalBombs,
                timeStarted = DateTime.Now
            };
        }

        #endregion Construction


        #region Update

        // Called when the player selects or flags a tile
        public void OnGameInput()
        {
            // We wait to start the game timer until the user inputs
            time.BeginGameTimer();
        }

        /// <summary>
        /// Called from client when the view needs an update on the game state
        /// </summary>
        public void UpdateGame()
        {
            // Update time elapsed
            time.UpdateGameTimeElapsed();
            
            // Push changes to view
            _view.UpdateGame(currentData);
        }

        #endregion Update


        #region State

        void IGameSystem.BeginGame()
        {
            //
            currentData = new GameProgressData
            {
                flagsRemaining = board.flagsRemaining
            };
            UpdateGame();
            
            state = GameState.InProgress;
        }

        void IGameSystem.GameLost()
        {
            state = GameState.Lose;
            time.EndGameTimer();
            OnGameEnd(false);
        }

        void IGameSystem.GameWon()
        {
            state = GameState.Win;
            time.EndGameTimer();
            OnGameEnd(true);
        }

        #endregion State


        #region End Game

        private void OnGameEnd(bool win)
        {
            GameSummaryData data = new GameSummaryData
            {
                didWin = win,
                secondsElapsed = (int)currentData.timeElapsed.TotalSeconds,
                bombsFlagged = board.GetNumberOfFlaggedBombs(),
                percentageFlagged = board.GetNumberOfFlaggedBombs() / (float)board.totalBombs
            };
            _view.OnGameFinished(data);
        }

        #endregion End Game
    }
}