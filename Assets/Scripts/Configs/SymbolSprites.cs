using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SymbolSprites", menuName = "Configs/SymbolSprites")]
    public class SymbolSprites : ScriptableObject
    {
        public List<Sprite> Sprites;
    }
}