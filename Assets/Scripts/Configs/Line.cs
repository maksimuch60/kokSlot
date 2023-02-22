// ReSharper disable once RedundantUsingDirective
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Line", menuName = "Configs/Line")]
    public class Line : ScriptableObject
    {
        public List<int> SlotPosition;
    }
}