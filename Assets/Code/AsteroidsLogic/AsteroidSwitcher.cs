using Assets.Code.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidSwitcher : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _asteroids = new List<GameObject>();
        [SerializeField] private GameObject _bigAsteroid;
        private AsteroidWatcher _asteroidWatcher;

        private void Start()
        {
            _asteroidWatcher = FindObjectOfType<GameEntryPoint>().Game.AsteroidWatcher;
        }

        private void OnEnable()
        {
            PrepareAsteroid();
        }

        public void CheckAsteroids()
        {
            Debug.Log("Switcher Check!");

            foreach (var asteroid in _asteroids)
            {
                if (asteroid.activeSelf == true)
                    return;
            }

            Debug.Log("Switcher willDeactivate!");

            DeactivateAsteroid();
        }
         
        private void PrepareAsteroid()
        {
            Debug.Log("Prepare");
            foreach (var asteroid in _asteroids)
            {
                asteroid.transform.localPosition = new Vector2(0,0);
                asteroid.SetActive(false);
            }

            _bigAsteroid.SetActive(true);
        }

        private void DeactivateAsteroid()
        {
            Debug.Log("Deactivated");

            gameObject.SetActive(false);
            _asteroidWatcher.CheckAsteroidsCount();
        }
    }
}