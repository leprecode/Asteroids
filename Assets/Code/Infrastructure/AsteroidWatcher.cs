using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class AsteroidWatcher
    {
        private List<GameObject> _pooledAsteroids;
        private AsteroidSpawner _asteroidSpawner;
        private WaveManager _waveManager;

        public void GetAllDependencies(AsteroidSpawner asteroidSpawner, WaveManager waveManager)
        {
            _asteroidSpawner = asteroidSpawner;
            _waveManager = waveManager;

            GetCreatedAsteroids();
        }

        private void GetCreatedAsteroids()
        {
            _pooledAsteroids = new List<GameObject>();

            _pooledAsteroids = _asteroidSpawner.pooledAsteroids;
        }

        public void CheckAsteroidsCount()
        {
            Debug.Log("CheckAsteroids!");

            for (int i = 0; i < _pooledAsteroids.Count; i++)
            {
                if (_pooledAsteroids[i].activeSelf)
                    return;
            }

            _waveManager.CreateNewAsteroidWave();
        }
    }
}