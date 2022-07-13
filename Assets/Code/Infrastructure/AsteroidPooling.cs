using Assets.Code.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class AsteroidPooling : MonoBehaviour
    {
        private readonly Transform _parentOfAsteroids;
        private readonly GameObject _bigAsteroidPrefab;
        private const int _poolingCountOfBigAsteroids = 50;

        public List<GameObject> pooledAsteroids { get; private set; }
        private Vector2 _startPosition = new Vector2(0, 0);

        public AsteroidPooling(Transform parentOfAsteroids, GameObject bigAsteroidPrefab)
        {
            _parentOfAsteroids = parentOfAsteroids;
            _bigAsteroidPrefab = bigAsteroidPrefab;

            PoolingAsteroids(_poolingCountOfBigAsteroids);
        }

        private void PoolingAsteroids(int countToCreate)
        {
            pooledAsteroids = new List<GameObject>();

            for (int i = 0; i < countToCreate; i++)
            {
                GameObject newAsteroid = Instantiate(_bigAsteroidPrefab, _startPosition, Quaternion.identity, _parentOfAsteroids);
                newAsteroid.SetActive(false);

                pooledAsteroids.Add(newAsteroid);
            }
        }
    }
}