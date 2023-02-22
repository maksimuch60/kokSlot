using System;
using System.Collections.Generic;
using Configs;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlotMachine
{
    public class Reel : MonoBehaviour
    {
        [SerializeField] private ReelSlotsPool _reelSlotsPool;
        [SerializeField] private List<Slot> _reelSlots;

        [Header("Move Animation Settings")]
        [SerializeField] private float _spinTime;
        [SerializeField] private float _spinSpeed;
        [SerializeField] private float _bounceTime;
        [SerializeField] private float _bounceHeight;
        
        private bool _isSpinning;
        private bool _isStopped = true;
        
        private float _tickTimer;
        private int _startSlotIndex;
        private int _currentSlotIndex;
        private int _stopSlotIndex;
        private int _lastSlotsCounter;

        public event Action<int> OnStopIndexCalculated;

        public int StopSlotIndex => _stopSlotIndex;

        private void Awake()
        {
            InitSlots();
        }

        private void Update()
        {
            if (!_isSpinning && _isStopped)
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

            if (!_isSpinning && !_isStopped)
            {
                _lastSlotsCounter++;
            }
            
            _currentSlotIndex++;
            
            _currentSlotIndex %= _reelSlotsPool.SymbolList.Count;
        }

        private int CalculateReelIndex(int randomIndex)
        {
            if (randomIndex < 4)
            {
                randomIndex += _reelSlotsPool.SymbolList.Count - 1;
            }
            
            return randomIndex - 4;
        }

        private void AnimateMoving()
        {
            _tickTimer += Time.deltaTime;

            if (!_isSpinning && _lastSlotsCounter < 1)
            {
                _currentSlotIndex = _stopSlotIndex;
            }
            
            foreach (Slot reelSlot in _reelSlots)
            {
                reelSlot.gameObject.transform.Translate(Vector3.down * _spinSpeed * Time.deltaTime);
                if (reelSlot.gameObject.transform.position.y < -3.5f)
                {
                    reelSlot.gameObject.transform.position = new Vector3(transform.position.x, 2.5f, 0);
                    foreach (Slot reelSlot1 in _reelSlots)
                    {
                        reelSlot1.ChangePosition();
                    }
                    SetSlotSprite(reelSlot);
                }
            }

            if (_tickTimer > _spinTime)
            {
                _isSpinning = false;
            }

            if (_lastSlotsCounter > 5)
            {
                _isStopped = true;
                PerformBounce();
            }
        }

        private void ResetSpin()
        {
            _tickTimer = 0;
            _lastSlotsCounter = 0;
        }

        private void PerformBounce()
        {
            foreach (Slot reelSlot in _reelSlots)
            {
                reelSlot.gameObject.transform.
                    DOMoveY(reelSlot.transform.position.y + _bounceHeight, _bounceTime / 2).
                    SetEase(Ease.OutQuad).
                    OnComplete(() =>
                    {	
                        reelSlot.gameObject.transform.
                            DOMoveY(reelSlot.transform.position.y - _bounceHeight, _bounceTime / 2).
                            SetEase(Ease.InQuad);
                    });
            }
        }

        public void Spin()
        {
            int randomIndex = Random.Range(0, _reelSlotsPool.SymbolList.Count);

            _stopSlotIndex = CalculateReelIndex(randomIndex);
            OnStopIndexCalculated?.Invoke(_stopSlotIndex);
            
            ResetSpin();

            _isSpinning = true;
            _isStopped = false;
        }
    }
}