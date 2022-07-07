using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidSwitcher : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _asteroids = new List<GameObject>();
        [SerializeField] private GameObject _bigAsteroid;

        private void Start()
        {
            SubscribeToAsteroids();
        }

        private void OnEnable()
        {
            PrepareAsteroid();
        }

        private void SubscribeToAsteroids()
        {
            Debug.Log("Subscribed!");

            foreach (var asteroid in _asteroids)
            {
                AsteroidDamageHandler.AsteroidDestroyed += CheckAsteroids;
            }
        }

        private void CheckAsteroids()
        {
            foreach (var asteroid in _asteroids)
            {
                if (asteroid.activeSelf == true)
                    return;
            }

            Invoke("DeactivateAsteroid",0.1f);

        }

        private void PrepareAsteroid()
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid.SetActive(false);
            }

            _bigAsteroid.SetActive(true);
        }

        private void DeactivateAsteroid()
        {
            gameObject.SetActive(false);
        }
    }
}