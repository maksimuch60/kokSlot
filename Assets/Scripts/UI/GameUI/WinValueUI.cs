using System.Globalization;
using Game.SlotMachine;
using TMPro;
using UnityEngine;

namespace UI.GameUI
{
    public class WinValueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winValueLabel;
        [SerializeField] private WinChecker _winChecker;

        private void OnEnable()
        {
            _winChecker.OnValueChanged += SetWinValue;
        }

        private void OnDisable()
        {
            _winChecker.OnValueChanged -= SetWinValue;
        }

        private void SetWinValue(float winValue)
        {
            _winValueLabel.text = winValue.ToString();
        }
    }
}