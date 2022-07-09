using Assets.Code.AsteroidsLogic;
using Assets.Code.PoolingLogic;
using Assets.Code.Services;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private StageData _stageData;
        private const string TagToSearchParentOfAsteroids = "POOL_ASTEROIDS";
        private Game _game;

        public StageData StageData { get => _stageData;}

        private void Awake()
        {
            var mainCamera = Camera.main;

            var parentOfAsteroids = GameObject.FindGameObjectWithTag(TagToSearchParentOfAsteroids).transform;

            _game = new Game(new ScreenService(mainCamera),
                new AsteroidPooling(parentOfAsteroids, _stageData.BigAsteroidPrefab),
                new AsteroidSpawner(),
                new AsteroidWatcher(),
                new PlayerWatcher());
        }
    }
}