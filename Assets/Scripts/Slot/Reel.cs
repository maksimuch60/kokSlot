using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Slot
{
    public class Reel : MonoBehaviour
    {
        [SerializeField] private ReelSlotsPool _reelSlotsPool;
        [SerializeField] private List<Slot> _reelSlots;

        [SerializeField] private float _spinTime;
        [SerializeField] private float _spinSpeed;
        
        private bool _isSpinning;

        private void Awake()
        {
            InitSlots();
        }

        private void Update()
        {
            if (!_isSpinning)
                return;
            
            
        }

        private void InitSlots()
        {
            int randomIndex = Random.Range(0, _reelSlotsPool.SymbolList.Count);

            foreach (Slot reelSlot in _reelSlots)
            {
                reelSlot.SetSprite(_reelSlotsPool.GetSprite(randomIndex));
                randomIndex++;
                if (randomIndex >= _reelSlotsPool.SymbolList.Count)
                {
                    randomIndex = 0;
                }
            }
        }

        public void Spin()
        {
            int stopSlotIndex = Random.Range(0, _reelSlotsPool.SymbolList.Count);

            StartCoroutine(PlaySpinAnimation(() => { }));
        }

        private IEnumerator PlaySpinAnimation(Action onCompleteCallback)
        {
            yield return null;
        }
    }
}