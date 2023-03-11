using System;
using System.Collections.Generic;
using Game.SlotMachine;

namespace Configs
{
    [Serializable]
    public class SymbolSettings
    {
        public SymbolType SymbolType;
        public List<float> MultiplyArray;
    }
}