using Assets.Code.AsteroidsLogic;
using Assets.Code.Services;
using Assets.Code.UfoLogic;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class GameEntryPoint : MonoBehaviour
    {
        public static GameEntryPoint instance { get; private set; }
        [SerializeField] private StageData _stageData;
        private Game _game;

        public StageData StageData { get => _stageData;}
        public Game Game { get => _game; }

        private void Start()
        {
            var mainCamera = Camera.main;

            var parentOfAsteroids = GameObject.FindGameObjectWithTag("POOL_ASTEROIDS");
            var parentOfUfo = GameObject.FindGameObjectWithTag("POOL_UFO");
            var parentOfUfoBullets = GameObject.FindGameObjectWithTag("POOL_UFO_BULLETS");

            _game = new Game(new ScreenService(mainCamera),
                new AsteroidPooling(parentOfAsteroids.transform, _stageData.BigAsteroidPrefab),
                new AsteroidSpawner(),
                new AsteroidWatcher(),
                new PlayerWatcher(),
                new UfoPooling(parentOfUfo.transform,_stageData.UfoPrefab),
                new UfoSpawner(),
                new UfoBulletPooler(parentOfUfoBullets.transform, _stageData.UfoBulletPrefab),
                new WaveManager(),
                new UfoWatcher(),
                new Score());

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
                return;
            }

            Destroy(this.gameObject);
        }
    }
}