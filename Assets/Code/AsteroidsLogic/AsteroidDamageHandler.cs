using Assets.Code.BulletLogic;
using Assets.Code.Infrastructure;
using Assets.Code.Interfaces;
using System;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidDamageHandler : MonoBehaviour, IDamagable
    {
        [SerializeField] private GameObject _firstSmallerAsteroid;
        [SerializeField] private GameObject _secondSmallerAsteroid;
        [SerializeField] private AsteroidSwitcher _asteroidSwitcher;
        [SerializeField] private AudioSource _audioSource;

        public void ApplyDamage()
        {
            _audioSource.Play();

            if (_firstSmallerAsteroid != null && _secondSmallerAsteroid != null)
            {
                _firstSmallerAsteroid.SetActive(true);
                _secondSmallerAsteroid.SetActive(true);

                SetNewPosition();

                SetNewDirection();

                SetNewMovementSpeed();
            }

            this.gameObject.SetActive(false);
            _asteroidSwitcher.CheckAsteroids();
        }

        private void SetNewMovementSpeed()
        {
            var RandomSpeed = Mathf.Round(UnityEngine.Random.Range(1f, 4f) * 10) / 10;

            _firstSmallerAsteroid.GetComponent<AsteroidMover>().ApplySpeed(RandomSpeed);
            _secondSmallerAsteroid.GetComponent<AsteroidMover>().ApplySpeed(RandomSpeed);
        }

        private void SetNewPosition()
        {
            _firstSmallerAsteroid.transform.position = transform.position;
            _secondSmallerAsteroid.transform.position = transform.position;
        }

        private void SetNewDirection()
        {
            var lastDirection = transform.rotation.y;

            _firstSmallerAsteroid.GetComponent<AsteroidMover>().ChooseDirection(lastDirection);
            _secondSmallerAsteroid.GetComponent<AsteroidMover>().ChooseDirection(lastDirection);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable);

            if (damagable == null)
                return;

            damagable.ApplyDamage();
        }
    }
}