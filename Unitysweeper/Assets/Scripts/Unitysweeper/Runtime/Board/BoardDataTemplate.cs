using Sharpsweeper.Board.Data;
using UnityEngine;

namespace Unitysweeper.Board
{
    [CreateAssetMenu]
    public class BoardDataTemplate : ScriptableObject
    {
        public string displayLabel;
        public BoardData data;
    }
}