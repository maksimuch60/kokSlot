using Game.SlotMachine;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI
{
    public class SpinButton : MonoBehaviour
    {
        [SerializeField] private Button _spinButton;
        [SerializeField] private SlotMachine _slotMachine;
        

        private void Awake()
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
        }

        private void OnSpinButtonClicked()
        {
            _slotMachine.SpinAllReels();
        }
    }
}