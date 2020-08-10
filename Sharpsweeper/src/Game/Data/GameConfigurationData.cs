using System;
using Sharpsweeper.Board.Data;

namespace Sharpsweeper.Game.Data
{
    public struct GameConfigurationData
    {
        public BoardData boardData;
        public int totalBombs;
        public DateTime timeStarted;
    }
}