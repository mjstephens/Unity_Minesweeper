using UnityEngine;

namespace Unitysweeper.Board
{
    [CreateAssetMenu(
        fileName = "Board Style Data", 
        menuName = "Minesweeper/Board Style")]
    public class BoardViewDataTemplate : ScriptableObject
    {
        public float tileSpacing;
        public GameObject tilePrefab;
        public Color backgroundColor;
    }
}