using UnityEngine;

namespace Assets.Code.BulletLogic
{
    public class BulletWrapper : MonoBehaviour
    {
        public delegate void OnReturn();
        public event OnReturn Returned;
        private ScreenBounds _screenBounds;

        private void Start() => _screenBounds = FindObjectOfType<ScreenBounds>();

        void Update() => Wrapping();

        private void Wrapping()
        {
            if (_screenBounds.AmIOutOfBounds(transform.localPosition))
            {
                Vector2 newPosition = _screenBounds.CalculateWrappedPosition(transform.localPosition);
                transform.position = newPosition;
                Returned?.Invoke();
            }
        }
    }
}