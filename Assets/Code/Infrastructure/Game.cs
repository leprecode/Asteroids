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
        private readonly UfoPooling _ufoPooling;
        private readonly UfoSpawner _ufoSpawner;
        private readonly UfoBulletPooler _ufoBulletPooler;
        private readonly UfoWatcher _ufoWatcher;
        private readonly WaveManager _waveManager;

        public AsteroidWatcher AsteroidWatcher => _asteroidWatcher;

        public UfoWatcher UfoWatcher => _ufoWatcher;

        public ScreenService ScreenService => _screenService;

        public Game(ScreenService screenService, AsteroidPooling asteroidPooling,
            AsteroidSpawner asteroidSpawner, AsteroidWatcher asteroidWatcher,
            PlayerWatcher playerWatcher, UfoPooling ufoPooling, UfoSpawner ufoSpawner,
            UfoBulletPooler ufoBulletPooler, WaveManager waveManager, UfoWatcher ufoWatcher)
        {
            _screenService = screenService;
            _asteroidPooling = asteroidPooling;
            _asteroidSpawner = asteroidSpawner;
            _asteroidWatcher = asteroidWatcher;
            _playerWatcher = playerWatcher;
            _ufoPooling = ufoPooling;
            _ufoSpawner = ufoSpawner;
            _ufoBulletPooler = ufoBulletPooler;
            _waveManager = waveManager;
            _ufoWatcher = ufoWatcher;

            _asteroidSpawner.GetScreenService(_screenService);
            _asteroidSpawner.GetPooledAsteroids(_asteroidPooling.pooledAsteroids);
            _asteroidWatcher.GetAllDependencies(_asteroidSpawner, _waveManager);

            _ufoSpawner.GetScreenService(_screenService);
            _ufoSpawner.GetPooledUfo(_ufoPooling.pooledUfo);
            _ufoWatcher.GetAllDependencies(_ufoPooling, _waveManager);
                
            _waveManager.GetAllDependencies(_asteroidSpawner, _ufoSpawner);
            _waveManager.CreateFirstWave();
        }

    }
}