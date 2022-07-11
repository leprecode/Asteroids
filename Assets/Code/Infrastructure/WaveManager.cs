using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class WaveManager
    {
        private const int StartCountOfAsteroids = 2;
        private const float TimeBeforeCreatingNewWave = 2f;
        private const float MinTimeToCreateUfo = 20f;
        private const float MaxTimeToCreateUfo = 40f;

        public delegate void OnEndWave();
        public static event OnEndWave WaveEnd;

        private AsteroidSpawner _asteroidSpawner;
        private UfoSpawner _ufoSpawner;

        private int _waveNumber = -1;
        private bool _isUfoSpawned = false;

        public void GetAllDependencies(AsteroidSpawner asteroidSpawner, UfoSpawner ufoSpawner)
        {
            _asteroidSpawner = asteroidSpawner;
            _ufoSpawner = ufoSpawner;
        }

        public void CreateNewUfo()
        {
            var randomTimeToCreate = Random.Range(MinTimeToCreateUfo,MaxTimeToCreateUfo);

            Debug.Log("Time To Ufo: " + randomTimeToCreate);

            CoroutineRunner.StartRoutine(NewUfoCoroutine(randomTimeToCreate));
        }

        public void CreateNewAsteroidWave()
        {
            _waveNumber ++;
            var newCount = StartCountOfAsteroids + _waveNumber;

            CoroutineRunner.StartRoutine(NewWaveCoroutine(newCount));
        }

        public void CreateFirstWave()
        {
            _waveNumber++;
            var newCount = StartCountOfAsteroids + _waveNumber;
            _asteroidSpawner.CreateNewWave(newCount);

            CreateNewUfo();
        }

        private IEnumerator NewWaveCoroutine(int newCount)
        {
            yield return new WaitForSeconds(TimeBeforeCreatingNewWave);
            _asteroidSpawner.CreateNewWave(newCount);
        }

        private IEnumerator NewUfoCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _ufoSpawner.CreateUfo();
        }
    }
}