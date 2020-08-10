using Sharpsweeper.Game.Data;
using UnityEngine;
using Unitysweeper.Board;

namespace Unitysweeper.Serialization
{
    public static class GameHighScore
    {
        #region Variables

        private const string CONST_HighScoreDataFile = "scores";
        private const string CONST_HighScoreDataFormat = ".txt";
        
        #endregion Variables


        #region Score

        // Returns true if the score is a new best.
        public static bool ReportGameScoreData(GameSummaryData data, string difficultyKey)
        {
            // Is this a new high score?
            bool highScore = false;
            string scorePath = CONST_HighScoreDataFile + difficultyKey + CONST_HighScoreDataFormat;
            string previousScoreData = Serializer.ReadData(scorePath);
            if (string.IsNullOrEmpty(previousScoreData))
            {
                highScore = true;
            }
            else
            {
                GameSummaryData prevData = JsonUtility.FromJson<GameSummaryData>(previousScoreData);
                if (GetIsBetterScore(data, prevData))
                {
                    highScore = true;
                }
            }

            // Write data to file if it's a new high score
            if (highScore)
            {
                string dataBlob = JsonUtility.ToJson(data);
                Serializer.WriteData(dataBlob, scorePath);
            }

            return highScore;
        }


        private static bool GetIsBetterScore(GameSummaryData checkData, GameSummaryData sourceData)
        {
            return checkData.secondsElapsed < sourceData.secondsElapsed;
        }

        #endregion Score


        #region Utility

        public static string GetDifficultyKey(BoardDataTemplate data)
        {
            return "_" + data.displayLabel;
        }

        #endregion Utility
    }
}