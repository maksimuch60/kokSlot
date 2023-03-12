using System;
using System.Collections.Generic;
using Configs;
using Game.RateModule;
using UnityEngine;

namespace Game.SlotMachine
{
    public class WinChecker : MonoBehaviour
    {
        [SerializeField] private SlotMachine _slotMachine;
        [SerializeField] private MultipliesSettings _multipliesSettings;
        [SerializeField] private List<Line> _lines;

        private SymbolType[,] _lineSymbolsMatrix;
        private int[] _symbolCounterInRowArray;
        private SymbolType[] _linePaySymbol;
        private float _winValue;
        private float _spinMultiplier;
        private int _scatterCounter;

        public event Action<float> OnValueChanged;
        public event Action OnBonusGameWorked;

        public float WinValue => _winValue;
        public float SpinMultiplier => _spinMultiplier;
        public int ScatterCounter => _scatterCounter;

        private void Awake()
        {
            _lineSymbolsMatrix = new SymbolType[4, 5];
            _linePaySymbol = new SymbolType[_lines.Count];
        }

        private void OnEnable()
        {
            _slotMachine.OnEnableStopIndexes += CalculateWin;
        }

        private void OnDisable()
        {
            _slotMachine.OnEnableStopIndexes -= CalculateWin;
        }

        public void CalculateWin(SymbolType[,] playSymbolsMatrix)
        {
            _winValue = 0;
            _spinMultiplier = 0;
            _scatterCounter = 0;
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ComposeLineSymbols(playSymbolsMatrix[j, i], j, i);
                }
            }

            CheckLinesSymbols();
            CheckBonusGame(playSymbolsMatrix);
            PrintWin();
        }

        private void ComposeLineSymbols(SymbolType symbolType, int row, int reel)
        {
            for(int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].SlotPosition[reel] == row + 1)
                {
                    _lineSymbolsMatrix[i, reel] = symbolType;
                }
            }
        }

        private void CheckBonusGame(SymbolType[,] playSymbolsMatrix)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (playSymbolsMatrix[j,i] == SymbolType.MajorFive)
                    {
                        _scatterCounter++;
                    }
                }
            }
        }

        private void CheckLinesSymbols()
        {
            _symbolCounterInRowArray = new int[_lines.Count];
            for (int i = 0; i < _lines.Count; i++)
            {
                SymbolType symbolType = _lineSymbolsMatrix[i, 0];
                _linePaySymbol[i] = symbolType;
                _symbolCounterInRowArray[i]++;
                for (int j = 1; j < 5; j++)
                {
                    if (_lineSymbolsMatrix[i, j] == symbolType)
                    {
                        _symbolCounterInRowArray[i]++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void PrintWin()
        {
            for(int i = 0; i < _symbolCounterInRowArray.Length; i++)
            {
                if (_symbolCounterInRowArray[i] > 1)
                {
                    CalculateMultiplier(_linePaySymbol[i], _symbolCounterInRowArray[i]);
                }
            }

            PrintBonusGame();
            ChangeWinValue();
        }

        private void PrintBonusGame()
        {
            if (_scatterCounter >= 3)
            {
                OnBonusGameWorked?.Invoke();
            }
        }

        private void ChangeWinValue()
        {
            OnValueChanged?.Invoke(_winValue);
        }

        private void CalculateMultiplier(SymbolType paySymbol, int symbolCounterInRow)
        {
            foreach (SymbolSettings symbolSetting in _multipliesSettings.MultipliesSettingsList)
            {
                if (symbolSetting.SymbolType == paySymbol)
                {
                    float multiplier = symbolSetting.MultiplyArray[symbolCounterInRow - 1];
                    _spinMultiplier += multiplier;
                    _winValue += multiplier * Rate.CurrentRate;
                }
            }
        }
    }
}