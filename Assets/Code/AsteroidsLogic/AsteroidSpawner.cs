using Assets.Code.PoolingLogic;
using Assets.Code.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidSpawner
    {
        //From size of asteroid from mesh bounds
        private const float RadiusToCheck = 4.0f;
        private const int _layerMaskOfPlayer = 64;
        private List<GameObject> _pooledAsteroids;
        public List<GameObject> activatedAsteroids { get; private set; }
        private ScreenService _screenService;

        private float _leftPointX;
        private float _rightPointX;
        private float _topPointY;
        private float _bottomPointY;

        public AsteroidSpawner()
        {
            _pooledAsteroids = new List<GameObject>();
            activatedAsteroids = new List<GameObject>();
            
        }

        public void GetScreenService(ScreenService screenService)
        {
            _screenService = screenService;
            TakeAllPoints();
        }

        public void GetPooledAsteroids(List<GameObject> pooledAsteroids)
        {
            _pooledAsteroids = pooledAsteroids;
        }

        public void NewWaveOfAsteroids(int Count)
        {
            activatedAsteroids.Clear();

            for (int i = 0; i < Count; i++)
            {
                var positionToCheck = NewRandomPosition();

                while (CheckPlayerPosition(positionToCheck)==false)
                {
                    positionToCheck = NewRandomPosition();
                }

                _pooledAsteroids[i].SetActive(true);
                _pooledAsteroids[i].transform.position = positionToCheck;
                activatedAsteroids.Add(_pooledAsteroids[i]);
            }
        }

        private bool CheckPlayerPosition(Vector2 positionToCheck)
        {
            var hited = Physics2D.OverlapCircle(positionToCheck, RadiusToCheck, _layerMaskOfPlayer);

            if (hited!=null)
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
            var RandomPlaceOnX = Random.Range(_leftPointX, _rightPointX);
            var RandomPlaceOnY = Random.Range(_bottomPointY, _topPointY);

            var RandomPosition = new Vector2(RandomPlaceOnX, RandomPlaceOnY);

            return RandomPosition;
        }

        private void TakeAllPoints()
        {
            _leftPointX = _screenService._leftPointX;
            _rightPointX = _screenService._rightPointX;
            _topPointY = _screenService._topPointY;
            _bottomPointY = _screenService._bottomPointY;
        }
    }
}