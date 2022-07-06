using Assets.Code.AsteroidsLogic;
using Assets.Code.PoolingLogic;
using Assets.Code.Services;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class Game
    {
        private readonly GameEntryPoint _gameEntryPoint;

        private AsteroidPooling _asteroidPooling;
        private AsteroidSpawner _asteroidSpawner;
        private AsteroidWatcher _asteroidWatcher;

        private ScreenService _screenService;

        public Game(ScreenService screenService, AsteroidPooling asteroidPooling, AsteroidSpawner asteroidSpawner, AsteroidWatcher asteroidWatcher, GameEntryPoint gameEntryPoint)
        {
            this._screenService = screenService;
            this._asteroidPooling = asteroidPooling;
            this._asteroidSpawner = asteroidSpawner;
            this._asteroidWatcher = asteroidWatcher;
            _gameEntryPoint = gameEntryPoint;

            _asteroidSpawner.GetScreenService(_screenService);
            _asteroidSpawner.GetPooledAsteroids(_asteroidPooling.pooledAsteroids);
            _asteroidSpawner.NewWaveOfAsteroids(2);
            //_asteroidSpawner.NewWaveOfAsteroids(_gameEntryPoint.StageData.StartCountOfAsteroids);
            _asteroidWatcher.GetAsteroidSpawner(_asteroidSpawner);
        }
    }
}