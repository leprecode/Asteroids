using System;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerOffScreenReturn : MonoBehaviour
    {
        public delegate void ChangeVisible();
        public static event ChangeVisible OnInvisible;
        public static event ChangeVisible OnVisible;

        private const float _timeToResetPosition = 0.5f;
        private const float _timeToResetToWorldCenter = 5f;

        [SerializeField] private Renderer _spriteRenderer;
        [SerializeField] private PlayerSpawnBehaviour _spawnBehaviour;

        private Camera _camera;
        private Vector2 _lastPosition;
        private float _lastZRotation;
        private bool _isInvisible = false;
        private float _timer;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_isInvisible)
            {
                _timer += Time.deltaTime;

                if (_timer >= _timeToResetPosition)
                {
                    ResetPlayer();
                }

                if (_timer >= _timeToResetToWorldCenter)
                {
                    transform.position = new Vector3(0, 0, 0);
                    _spawnBehaviour.enabled = true;
                }
            }
        }

        private void ResetPlayer()
        {
            transform.rotation = Quaternion.Euler(0,0,-_lastZRotation);
            transform.position = -_lastPosition;

            Debug.Log("ResetPlayer");
        }

        private void OnBecameInvisible()
        {
            Debug.Log("InvisPlayer");

            _lastZRotation =  transform.rotation.eulerAngles.z;
            _lastPosition = transform.position;
            _isInvisible = true;

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
            Debug.Log("VisPlayer");

            OnVisible?.Invoke();
            _isInvisible = false;
            _timer = 0f;
        }
    }
}


