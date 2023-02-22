using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlotMachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private List<Reel> _reels;

        private List<int> _stopIndexes = new();

        public event Action<List<int>> OnEnableStopIndexes;

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
                OnEnableStopIndexes?.Invoke(_stopIndexes);
            }
        }

        public void SpinAllReels()
        {
            foreach (Reel reel in _reels)
            {
                reel.Spin();
            }
        }
    }
}