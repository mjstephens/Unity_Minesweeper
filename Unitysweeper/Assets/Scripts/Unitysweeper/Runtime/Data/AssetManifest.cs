using System.Collections.Generic;
using UnityEngine;

namespace Unitysweeper.Data
{
    [CreateAssetMenu(
        fileName = "Asset Manifest", 
        menuName = "Minesweeper/Manifests/Asset Manifest", 
        order = 0)]
    public class AssetManifest : ScriptableObject
    {
        public List<Object> manifest = new List<Object>();
    }
}