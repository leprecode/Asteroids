using Assets.Code.UI.Menu;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class WaveManager
    {
        public delegate void OnEndWave();
        public static event OnEndWave WaveEnd;

        private const int StartCountOfAsteroids = 2;
        private const float TimeBeforeCreatingNewWave = 2f;
        private const float MinTimeToCreateUfo = 20f;
        private const float MaxTimeToCreateUfo = 40f;

        private AsteroidSpawner _asteroidSpawner;
        private AsteroidWatcher _asteroidWatcher;
        private UfoSpawner _ufoSpawner;
        private UfoWatcher _ufoWatcher;

        private int _waveNumber = 0;
        private bool _asteroidsDestroyed = false;
        private bool _ufoDestroyed = false;

        public bool AsteroidsDestroyed { get => _asteroidsDestroyed; set => _asteroidsDestroyed = value; }
        public bool UfoDestroyed { get => _ufoDestroyed; set => _ufoDestroyed = value; }

        public void GetAllDependencies(AsteroidSpawner asteroidSpawner, UfoSpawner ufoSpawner,
            UfoWatcher ufoWatcher, AsteroidWatcher asteroidWatcher)
        {
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
            _ufoWatcher = ufoWatcher;
            _asteroidWatcher = asteroidWatcher;
            Menu.RestartGame += Restart;
        }

        public void CreateNewUfo()
        {
            var randomTimeToCreate = Random.Range(MinTimeToCreateUfo, MaxTimeToCreateUfo);

            Debug.Log("Time To Ufo: " + randomTimeToCreate);

            CoroutineRunner.StartNewRoutine(NewUfoCoroutine(randomTimeToCreate));
        }

        public void CheckWave()
        {
            if (_asteroidsDestroyed && _ufoDestroyed)
            {
                CreateNewAsteroidWave();
                CreateNewUfo();
            }
        }

        public void CreateNewAsteroidWave()
        {
            _waveNumber++;
            var newCount = StartCountOfAsteroids + _waveNumber;

            CoroutineRunner.StartNewRoutine(NewWaveCoroutine(newCount));
        }

        public void CreateFirstWave()
        {
            var newCount = StartCountOfAsteroids + _waveNumber;
            _asteroidSpawner.CreateNewWave(newCount);

            CreateNewUfo();
        }

        private void Restart()
        {
            _waveNumber = 0;

            CoroutineRunner.StopAllRoutines();            
            CreateFirstWave();
        }

        private IEnumerator NewWaveCoroutine(int newCount)
        {
            yield return new WaitForSeconds(TimeBeforeCreatingNewWave);
            _asteroidSpawner.CreateNewWave(newCount);
            _asteroidsDestroyed = false;
        }

        private IEnumerator NewUfoCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _ufoSpawner.CreateUfo();
            _ufoDestroyed = false;
        }
    }
}