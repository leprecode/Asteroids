using Assets.Code.PoolingLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidWatcher
    {
        private List<GameObject> _createdAsteroids;
        private AsteroidSpawner _asteroidSpawner;

        public AsteroidWatcher(AsteroidSpawner asteroidSpawner)
        {
            _asteroidSpawner = asteroidSpawner;
        }
    }
}