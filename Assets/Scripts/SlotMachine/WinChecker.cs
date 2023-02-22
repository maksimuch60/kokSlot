using System;
using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace SlotMachine
{
    public class WinChecker : MonoBehaviour
    {
        [SerializeField] private SlotMachine _slotMachine;

        [SerializeField] private List<Line> _lines;

        private void OnEnable()
        {
            _slotMachine.OnEnableStopIndexes += CheckLines;
        }

        private void OnDisable()
        {
            _slotMachine.OnEnableStopIndexes -= CheckLines;
        }

        private void CheckLines(List<int> stopIndexes)
        {
            //array = GetPlaySlotsArray(stopIndexes);
            //for()
            // {
            //     for()
            //      {
            //          Check for coincidence
            //      }
            // }
        }
    }
}