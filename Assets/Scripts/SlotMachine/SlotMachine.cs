using System.Collections.Generic;
using UnityEngine;

namespace SlotMachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private List<Reel> _reels;

        public void SpinAllReels()
        {
            foreach (Reel reel in _reels)
            {
                reel.Spin();
            }
        }
    }
}