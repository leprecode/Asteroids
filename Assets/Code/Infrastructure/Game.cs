using Assets.Code.AsteroidsLogic;
using Assets.Code.PoolingLogic;
using Assets.Code.Services;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class Game
    {
        private const int StartCountOfAsteroids = 2;

        private readonly AsteroidPooling _asteroidPooling;
        private readonly AsteroidSpawner _asteroidSpawner;
        private readonly AsteroidWatcher _asteroidWatcher;
        private readonly PlayerWatcher _playerWatcher;
        private readonly ScreenService _screenService;

        public Game(ScreenService screenService, AsteroidPooling asteroidPooling, 
            AsteroidSpawner asteroidSpawner, AsteroidWatcher asteroidWatcher, 
            PlayerWatcher playerWatcher)
        {
            _screenService = screenService;
            _asteroidPooling = asteroidPooling;
            _asteroidSpawner = asteroidSpawner;
            _asteroidWatcher = asteroidWatcher;
            _playerWatcher = playerWatcher;

            _asteroidSpawner.GetScreenService(_screenService);
            _asteroidSpawner.GetPooledAsteroids(_asteroidPooling.pooledAsteroids);
            _asteroidSpawner.NewWaveOfAsteroids(StartCountOfAsteroids);
            _asteroidWatcher.GetAsteroidSpawner(_asteroidSpawner);
        }
    }
}