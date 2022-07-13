using Assets.Code.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class UfoSpawner
    {
        private const float RadiusToCheck = 4.0f;
        private const int _layerMaskOfPlayer = 64;
        
        private List<GameObject> _pooledUfo;
        private ScreenService _screenService;

        private float _leftPointX;
        private float _rightPointX;
        private float _maxPointY;
        private float _minPointY;

        public UfoSpawner()
        {
            _pooledUfo = new List<GameObject>();
        }

        public void GetScreenService(ScreenService screenService)
        {
            _screenService = screenService;
            TakeAllPoints();
        }

        public void GetPooledUfo(List<GameObject> pooledUfo)
        {
            _pooledUfo = pooledUfo;
        }

        public void CreateUfo()
        {
            var positionToCheck = NewRandomPosition();

            while (CheckPlayerPosition(positionToCheck) == false)
            {
                positionToCheck = NewRandomPosition();
            }

            for (int i = 0; i < _pooledUfo.Count; i++)
            {
                if (_pooledUfo[i].activeSelf == false)
                {
                    _pooledUfo[i].SetActive(true);
                    _pooledUfo[i].transform.position = positionToCheck;
                    return;
                }
            }
        }

        private bool CheckPlayerPosition(Vector2 positionToCheck)
        {
            var hited = Physics2D.OverlapCircle(positionToCheck, RadiusToCheck, _layerMaskOfPlayer);

            if (hited != null)
            {
                Debug.Log("Hited with" + hited.name);
            }

            if (hited == null)
                return true;
            else
                return false;
        }

        private Vector3 NewRandomPosition()
        {
            var RandomPlaceOnX = Random.Range(0f, 100f);

            if (RandomPlaceOnX <= 50)
                RandomPlaceOnX = _leftPointX;
            else
                RandomPlaceOnX = _rightPointX;

            var RandomPlaceOnY = Random.Range(_minPointY, _maxPointY);

            var RandomPosition = new Vector2(RandomPlaceOnX, RandomPlaceOnY);

            return RandomPosition;
        }

        private void TakeAllPoints()
        {
            _leftPointX = _screenService._leftPointX;
            _rightPointX = _screenService._rightPointX;

            _maxPointY = _screenService._maxUfoPointYInWorldSpace;
            _minPointY = _screenService._minUfoPointYInWorldSpace;
        }
    }
}
