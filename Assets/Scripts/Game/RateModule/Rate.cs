using UnityEngine;

namespace Game.RateModule
{
    public static class Rate
    {
        public static readonly int[] RateArray = {10, 20, 30, 40, 50};
        private static int _currentRate;
        public static int CurrentRate => _currentRate;
        

        public static int GetRate(int index)
        {
            _currentRate = RateArray[index];
            return RateArray[index];
        }
    }
}