using System.Collections.Generic;
using Configs;
using Game.RateModule;
using Game.SlotMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.SimulationModule
{
    public class Simulation : MonoBehaviour
    {
        [SerializeField] private List<ReelSlotsPool> _reelSlotsPools;
        [SerializeField] private WinChecker _winChecker;
        [SerializeField] private int _simulationSpinCount;

        [SerializeField] private float[] _multipliersRange = {0, 0.5f, 1, 2, 3, 5, 10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 100, 150, 250, 500};
        private List<int> _stopIndexes;
        private float _totalWin;
        private int _bonusGameCounter;
        private readonly Dictionary<float, int> _spinSpreadingTable = new();

        private void Awake()
        {
            foreach (float range in _multipliersRange)
            {
                _spinSpreadingTable.Add(range, 0);
            }
        }

        public void Simulate()
        {
            ClearTableValues();
            
            SymbolType[,] symbolMatrix = new SymbolType[4,5];
            for (int i = 0; i < _simulationSpinCount; i++)
            {
                CreatePlaySymbolMatrix(symbolMatrix);
                _winChecker.CalculateWin(symbolMatrix);
                SpacingDefinition(_winChecker.SpinMultiplier);
                _totalWin += _winChecker.WinValue;

                if (_winChecker.ScatterCounter >= 3)
                {
                    _bonusGameCounter++;
                }
            }

            foreach (KeyValuePair<float,int> pair in _spinSpreadingTable)
            {
                Debug.Log($" <= {pair.Key} --- {pair.Value}");
            }
            
            Debug.Log($"rtp: {_totalWin/(_simulationSpinCount * Rate.CurrentRate)}");
            Debug.Log($"Bonus game hit cycle: {_simulationSpinCount / _bonusGameCounter}");
        }

        private void ClearTableValues()
        {
            foreach (float range in _multipliersRange)
            {
                _spinSpreadingTable[range] = 0;
            }
        }

        private void CreatePlaySymbolMatrix(SymbolType[,] symbolMatrix)
        {
            for (int k = 0; k < _reelSlotsPools.Count; k++)
            {
                int randomIndex = Random.Range(0, _reelSlotsPools[k].SymbolList.Count);
                for (int j = 0; j < 4; j++)
                {
                    symbolMatrix[j, k] = _reelSlotsPools[k].SymbolList[randomIndex++];
                    randomIndex %= _reelSlotsPools[k].SymbolList.Count;
                }
            }
        }

        private void SpacingDefinition(float spinMultiplier)
        {
            if (spinMultiplier == 0)
            {
                _spinSpreadingTable[0]++;
            }

            float previousRange = 0;
            for(int i = 1; i < _multipliersRange.Length; i++)
            {
                if (spinMultiplier > previousRange && spinMultiplier <= _multipliersRange[i])
                {
                    _spinSpreadingTable[_multipliersRange[i]]++;
                    break;
                }

                previousRange = _multipliersRange[i];
            }
        }
    }
}