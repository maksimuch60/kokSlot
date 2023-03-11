using System;
using System.Collections.Generic;
using Game.SlotMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Configs
{
    [CreateAssetMenu(fileName = "ReelSlotPool", menuName = "Configs/ReelSlotsPool")]
    public class ReelSlotsPool : ScriptableObject
    {
        [SerializeField] private SymbolSprites _symbolSprites;
        public List<SymbolType> SymbolList;

        /*private void OnValidate()
        {
            for(int i = 0; i < SymbolList.Count; i++)
            {
                SymbolList[i] = (SymbolType)Random.Range(0, (int)(SymbolType.Count));
            }
        }*/

        public Sprite GetSprite(int index)
        {
            return _symbolSprites.Sprites[(int)SymbolList[index]];
        }
    }
}