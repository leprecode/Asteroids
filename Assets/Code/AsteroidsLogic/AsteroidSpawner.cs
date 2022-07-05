using Assets.Code.PoolingLogic;
using Assets.Code.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidSpawner
    {
        private int _startCountOfAsteroids;
        private List<GameObject> _pooledAsteroids;
        private ScreenService _screenService;

        private float _leftPointX;
        private float _rightPointX;
        private float _topPointY;
        private float _bottomPointY;

        public AsteroidSpawner(AsteroidPooling asteroidPooling, int startCountOfAsteroids, ScreenService screenService)
        {
            _pooledAsteroids = new List<GameObject>();

            _pooledAsteroids = asteroidPooling.pooledAsteroids;
            _startCountOfAsteroids = startCountOfAsteroids;
            _screenService = screenService;


            TakeAllPoints();
            NewWaveOfAsteroids(_startCountOfAsteroids);
        }

        public void NewWaveOfAsteroids(int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                _pooledAsteroids[i].SetActive(true);

                NewRandomPosition(_pooledAsteroids[i]);
            }
        }

        private void NewRandomPosition(GameObject newAsteroid)
        {
            var RandomPlaceOnX = Random.Range(_leftPointX, _rightPointX);
            var RandomPlaceOnY = Random.Range(_bottomPointY, _topPointY);
            
            var RandomPosition = new Vector2(RandomPlaceOnX, RandomPlaceOnY);

            newAsteroid.transform.position = RandomPosition;

            Debug.Log("PlaceX" + RandomPlaceOnX);
            Debug.Log("PlaceY" + RandomPlaceOnY);
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