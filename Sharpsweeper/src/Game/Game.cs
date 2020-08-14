using System;
using Sharpsweeper.Board;
using Sharpsweeper.Board.Data;
using Sharpsweeper.Game.Data;
using Sharpsweeper.Game.View;

namespace Sharpsweeper.Game
{
    public class Game : IGameSimulation
    {
        #region Properties

        public IBoardSimulation board { get; }
        public GameState state { get; private set; }

        private IGameTimeSource time { get; }
        private readonly IGameView _view;

        #endregion Properties


        #region Data

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

            // Create the game time component
            time = new GameTime(this);
            
            // Set state view
            state = GameState.Waiting;
            _view = view;
            _view.GameSet(GetGameConfigurationData(boardData, board));
        }

        private static GameConfigurationData GetGameConfigurationData(BoardData bd, IBoardSimulation board)
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

        void IGameSimulation.BeginGame()
        {
            //
            currentData = new GameProgressData
            {
                flagsRemaining = board.flagsRemaining
            };
            UpdateGame();
            
            state = GameState.InProgress;
        }

        void IGameSimulation.GameLost()
        {
            state = GameState.Lose;
            time.EndGameTimer();
            OnGameEnd(false);
        }

        void IGameSimulation.GameWon()
        {
            state = GameState.Win;
            time.EndGameTimer();
            OnGameEnd(true);
        }

        #endregion State


        #region End Game

        public static bool CheckVictoryConditions(IBoardSimulation board)
        {
            return board.flaggedBombs >= board.totalBombs;
        }
        
        private void OnGameEnd(bool win)
        {
            GameSummaryData data = new GameSummaryData
            {
                didWin = win,
                secondsElapsed = (int)currentData.timeElapsed.TotalSeconds,
                bombsFlagged = board.flaggedBombs,
                percentageFlagged = board.flaggedBombs / (float)board.totalBombs
            };
            _view.OnGameFinished(data);
        }

        #endregion End Game
    }
}