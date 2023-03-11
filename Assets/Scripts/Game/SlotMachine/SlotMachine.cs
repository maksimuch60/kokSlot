using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SlotMachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private List<Reel> _reels;

        private readonly List<int> _stopIndexes = new();

        public event Action<SymbolType[,]> OnEnableStopIndexes;

        private void OnEnable()
        {
            foreach (Reel reel in _reels)
            {
                reel.OnStopIndexCalculated += SetStopIndex;
            }
        }

        private void OnDisable()
        {
            foreach (Reel reel in _reels)
            {
                reel.OnStopIndexCalculated -= SetStopIndex;
            }
        }

        private void SetStopIndex(int stopIndex)
        {
            _stopIndexes.Add(stopIndex);
            
            if (_stopIndexes.Count == 5)
            {
                int reelIndex = 0;
                SymbolType[,] symbolTypesMatrix = new SymbolType[4,5];
                foreach (Reel reel in _reels)
                {
                    SymbolType[] symbolTypesArray = reel.GetPlaySymbols();
                    for (int i = 0; i < 4; i++)
                    {
                        symbolTypesMatrix[i, reelIndex] = symbolTypesArray[i];
                    }
                    reelIndex++;
                }
                OnEnableStopIndexes?.Invoke(symbolTypesMatrix);
            }
        }

        public void SpinAllReels()
        {
            _stopIndexes.Clear();
            foreach (Reel reel in _reels)
            {
                reel.Spin();
            }
        }
    }
}