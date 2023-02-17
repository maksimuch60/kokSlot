using System;
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

        private float _tickTimer;
        private int _startSlotIndex;
        private int _currentSlotIndex;
        private int _stopSlotIndex;

        private void Awake()
        {
            InitSlots();
        }

        private void Update()
        {
            if (!_isSpinning)
                return;
            
            AnimateMoving();
        }

        private void InitSlots()
        {
            int randomIndex = Random.Range(0, _reelSlotsPool.SymbolList.Count);

            _startSlotIndex = CalculateReelIndex(randomIndex);
            _currentSlotIndex = _startSlotIndex;

            for(int i = _reelSlots.Count - 1; i >= 0; i--)
            {
                SetSlotSprite(_reelSlots[i]);
            }
        }

        private void SetSlotSprite(Slot slot)
        {
            slot.SetSprite(_reelSlotsPool.GetSprite(_currentSlotIndex));
            _currentSlotIndex++;
            if (_currentSlotIndex >= _reelSlotsPool.SymbolList.Count)
            {
                _currentSlotIndex = 0;
            }
        }

        private int CalculateReelIndex(int randomIndex)
        {
            if (randomIndex < 4)
            {
                randomIndex += _reelSlotsPool.SymbolList.Count - 1;
            }
            
            return randomIndex - 4;
        }

        public void Spin()
        {
            int randomIndex = Random.Range(0, _reelSlotsPool.SymbolList.Count);

            _stopSlotIndex = CalculateReelIndex(randomIndex);

            _isSpinning = true;
        }

        private void AnimateMoving()
        {
            _tickTimer += Time.deltaTime;
            
            foreach (Slot reelSlot in _reelSlots)
            {
                reelSlot.gameObject.transform.Translate(Vector3.down * _spinSpeed * Time.deltaTime);
                if (reelSlot.gameObject.transform.position.y < -3.5f)
                {
                    reelSlot.gameObject.transform.position = new Vector3(0, 2.5f, 0);
                    SetSlotSprite(reelSlot);
                }
            }

            if (_tickTimer > _spinTime)
            {
                _isSpinning = false;
                _tickTimer = 0;
            }
        }
    }
}