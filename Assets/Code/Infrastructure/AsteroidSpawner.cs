using Assets.Code.Services;
using Assets.Code.UI.Menu;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class AsteroidSpawner
    {
        private const float RadiusToCheck = 4.0f;
        private const int _layerMaskOfPlayer = 64;
        public List<GameObject> pooledAsteroids { get; private set; }
        private ScreenService _screenService;

        private float _leftPointX;
        private float _rightPointX;
        private float _topPointY;
        private float _bottomPointY;

        public AsteroidSpawner()
        {
            pooledAsteroids = new List<GameObject>();

            Menu.RestartGame += Restart;
        }

        private void Restart()
        {
            DeactivateAllAsteroids();
        }

        private void DeactivateAllAsteroids()
        {
            for (int i = 0; i < pooledAsteroids.Count; i++)
            {
                pooledAsteroids[i].SetActive(false);
            }
        }

        public void GetScreenService(ScreenService screenService)
        {
            _screenService = screenService;
            TakeAllPoints();
        }

        public void GetPooledAsteroids(List<GameObject> pooledAsteroids)
        {
            this.pooledAsteroids = pooledAsteroids;
        }

        public void CreateNewWave(int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                var positionToCheck = NewRandomPosition();

                while (CheckPlayerPosition(positionToCheck) == false)
                {
                    positionToCheck = NewRandomPosition();
                }

                pooledAsteroids[i].SetActive(true);
                pooledAsteroids[i].transform.position = positionToCheck;
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