using Game.RateModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI
{
    public class RateUI : MonoBehaviour
    {
        [SerializeField] private Button _increaseButton;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private TextMeshProUGUI _rateLabel;
        
        private int _currentIndex;

        private void Awake()
        {
            _increaseButton.onClick.AddListener(IncreaseButtonClicked);
            _decreaseButton.onClick.AddListener(DecreaseButtonClicked);

            _currentIndex = 0;
            SetRateText();
        }

        private void IncreaseButtonClicked()
        {
            if (_currentIndex < Rate.RateArray.Length - 1)
            {
                _currentIndex++;
            }
            
            SetRateText();
        }

        private void DecreaseButtonClicked()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
            }
            
            SetRateText();
        }

        private void SetRateText()
        {
            _rateLabel.text = Rate.GetRate(_currentIndex).ToString();
        }
    }
}