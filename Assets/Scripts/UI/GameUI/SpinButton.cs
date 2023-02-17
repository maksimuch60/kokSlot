using Slot;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI
{
    public class SpinButton : MonoBehaviour
    {
        [SerializeField] private Button _spinButton;
        [SerializeField] private Reel _reel;
        

        private void Awake()
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
        }

        private void OnSpinButtonClicked()
        {
            _reel.Spin();
        }
    }
}