using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MultipliesSettings", menuName = "Configs/MultipliesSettings")]
    public class MultipliesSettings : ScriptableObject
    {
        public List<SymbolSettings> MultipliesSettingsList;
    }
}