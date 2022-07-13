using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class AsteroidWatcher
    {
        private List<GameObject> _pooledAsteroids;
        private AsteroidSpawner _asteroidSpawner;
        private WaveManager _waveManager;
        private UfoWatcher _ufoWatcher;

        public void GetAllDependencies(AsteroidSpawner asteroidSpawner, WaveManager waveManager,
            UfoWatcher ufoWatcher)
        {
            _asteroidSpawner = asteroidSpawner;
            _waveManager = waveManager;
            _ufoWatcher = ufoWatcher;

            GetCreatedAsteroids();
        }

        public bool CheckAsteroidsCount()
        {
            Debug.Log("CheckAsteroids!");

            for (int i = 0; i < _pooledAsteroids.Count; i++)
            {
                if (_pooledAsteroids[i].activeSelf)
                    return false;
            }

            _waveManager.AsteroidsDestroyed = true;
            _ufoWatcher.CheckUfoCount();
            return true;
        }

        private void GetCreatedAsteroids()
        {
            _pooledAsteroids = new List<GameObject>();

            _pooledAsteroids = _asteroidSpawner.pooledAsteroids;
        }
    }
}