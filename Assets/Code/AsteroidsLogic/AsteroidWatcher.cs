using Assets.Code.PoolingLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidWatcher
    {
        private List<GameObject> _createdAsteroids;
        private AsteroidSpawner _asteroidSpawner;

        public void GetAsteroidSpawner(AsteroidSpawner asteroidSpawner)
        {
            _asteroidSpawner = asteroidSpawner;

            GetCreatedAsteroids();
        }

        private void GetCreatedAsteroids()
        {
            _createdAsteroids = new List<GameObject>();

            _createdAsteroids = _asteroidSpawner.activatedAsteroids;
        }
    }
}