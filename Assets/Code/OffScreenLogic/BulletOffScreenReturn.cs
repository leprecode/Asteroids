using Assets.Code.BulletLogic;
using UnityEngine;

namespace Assets.Code.OffScreenLogic
{
    public class BulletOffScreenReturn : MonoBehaviour
    {
        public delegate void OnReturn();
        public event OnReturn Returned;

        [SerializeField] private Renderer _spriteRenderer;
        [SerializeField] private Bullet _bullet;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnBecameInvisible()
        {
            var newWorldPosition = transform.position;
            var viewportPosition = _camera.WorldToViewportPoint(transform.position);

            if (viewportPosition.x < 0 || viewportPosition.x > 1)
                newWorldPosition.x = -newWorldPosition.x;

            if (viewportPosition.y < 0 || viewportPosition.y > 1)
                newWorldPosition.y = -newWorldPosition.y;


            transform.position = newWorldPosition;
            Returned?.Invoke();
        }
    } 
}


