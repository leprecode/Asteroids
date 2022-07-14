using Assets.Code.Services;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class Game
    {
        public static GameStates gameState;
        public static bool _isGameStarted = false;
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
        private readonly Score _score;

        public AsteroidWatcher AsteroidWatcher => _asteroidWatcher;

        public UfoWatcher UfoWatcher => _ufoWatcher;

        public Score Score => _score;

        public Game(ScreenService screenService, AsteroidPooling asteroidPooling,
            AsteroidSpawner asteroidSpawner, AsteroidWatcher asteroidWatcher,
            PlayerWatcher playerWatcher, UfoPooling ufoPooling, UfoSpawner ufoSpawner,
            UfoBulletPooler ufoBulletPooler, WaveManager waveManager, UfoWatcher ufoWatcher, 
            Score score)
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
            _score = score;

            _asteroidSpawner.GetScreenService(_screenService);
            _asteroidSpawner.GetPooledAsteroids(_asteroidPooling.pooledAsteroids);
            _asteroidWatcher.GetAllDependencies(_asteroidSpawner, _waveManager, _ufoWatcher);

            _ufoSpawner.GetScreenService(_screenService);
            _ufoSpawner.GetPooledUfo(_ufoPooling.pooledUfo);
            _ufoWatcher.GetAllDependencies(_ufoPooling, _waveManager);

            _waveManager.GetAllDependencies(_asteroidSpawner, _ufoSpawner, _ufoWatcher, _asteroidWatcher);
            _waveManager.CreateFirstWave();

            gameState = GameStates.onPause;
        }

    }
}