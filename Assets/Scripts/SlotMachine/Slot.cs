using UnityEngine;

namespace SlotMachine
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _positionInReel;

        public int PositionInReel => _positionInReel;

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void ChangePosition()
        {
            _positionInReel++;
            if (_positionInReel > 5)
            {
                _positionInReel = 0;
            }
        }
    }
}
