using System;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerOffScreenReturn : MonoBehaviour
    {
        [SerializeField] private Renderer _spriteRenderer;
        [SerializeField] private PlayerMove _playerMove;
        private Camera _camera;

        public delegate void ChangeVisible();
        public static event ChangeVisible OnInvisible;
        public static event ChangeVisible OnVisible;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnBecameInvisible()
        {
            OnInvisible?.Invoke();

            var newWorldPosition = transform.position;
            var viewportPosition = _camera.WorldToViewportPoint(transform.position);

            if (viewportPosition.x < 0 || viewportPosition.x > 1)
                newWorldPosition.x = -newWorldPosition.x;

            if (viewportPosition.y < 0 || viewportPosition.y > 1)
                newWorldPosition.y = -newWorldPosition.y;

            transform.position = newWorldPosition;
        }

        private void OnBecameVisible()
        {
            OnVisible?.Invoke();
        }
    }
}


