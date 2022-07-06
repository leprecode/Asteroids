using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class Shooting : MonoBehaviour
    {
        private const int CountOfBullets = 70;
        private List<GameObject> _poolOfBullets;

        [SerializeField] private GameObject _bulletsParent;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _socketForBullet;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _fireRate;

        private Vector2 _bulletSocketPosition => _socketForBullet.position;

        private int _lastSelectedBullet = -1;
        private float _timeToShoot;


        private void Awake()
        {
            _poolOfBullets = new List<GameObject>();

            BulletsPooling();
        }

        private void BulletsPooling()
        {
            _poolOfBullets = new List<GameObject>();

            for (int i = 0; i < CountOfBullets; i++)
            {
                var newBullet = Instantiate(_bulletPrefab);
                newBullet.transform.SetParent(_bulletsParent.transform);
                newBullet.name = "Bullet" + i;

                _poolOfBullets.Add(newBullet);
                _poolOfBullets[i].SetActive(false);
            }
        }

        public void Shoot()
        {
            if (Time.time > _timeToShoot)
            {
                _timeToShoot = Time.time + _fireRate;

                var newBullet = TakeNewBullet();

                newBullet.SetActive(false);
                newBullet.transform.position = _bulletSocketPosition;
                newBullet.transform.rotation = _socketForBullet.rotation;

                newBullet.SetActive(true);
                newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * _bulletSpeed);
            }
        }

        private GameObject TakeNewBullet()
        {
            _lastSelectedBullet++;

            if (_lastSelectedBullet == _poolOfBullets.Count - 1)
                _lastSelectedBullet = 0;

            return _poolOfBullets[_lastSelectedBullet];
        }
    }
}