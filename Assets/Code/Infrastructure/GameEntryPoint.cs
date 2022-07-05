using Assets.Code.AsteroidsLogic;
using Assets.Code.PoolingLogic;
using Assets.Code.Services;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class GameEntryPoint : MonoBehaviour
    {
        private const string TagToSearchParentOfAsteroids = "POOL_ASTEROIDS";
        [SerializeField] private StageData _stageData;
        private Game _game;

        private void Awake()
        {
            var mainCamera = Camera.main;
            var parentOfAsteroids = GameObject.FindGameObjectWithTag(TagToSearchParentOfAsteroids).transform;


            //Repair dependency
            _game = new Game(new ScreenService(mainCamera),
                new AsteroidPooling(parentOfAsteroids, _stageData.BigAsteroidPrefab), 
                new AsteroidSpawner(_game.asteroidPooling, _stageData.StartCountOfAsteroids, _game.screenService), 
                new AsteroidWatcher(_game.asteroidSpawner));       
        }
    }
}