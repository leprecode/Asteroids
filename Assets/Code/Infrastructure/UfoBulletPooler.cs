using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class UfoBulletPooler
    {
        private GameObject _bulletPrefab;
        private Transform _bulletsPoolParent;
        private const int _countOfBullets = 15;

        public UfoBulletPooler(Transform bulletsPoolParent, GameObject bulletPrefab)
        {
            _bulletsPoolParent = bulletsPoolParent;
            _bulletPrefab = bulletPrefab;

            CreateBullets();
        }

        private void CreateBullets()
        {
            for (int i = 0; i < _countOfBullets; i++)
            {
                var newBullet = Object.Instantiate(_bulletPrefab, _bulletsPoolParent.position, Quaternion.identity, _bulletsPoolParent.transform);
                newBullet.SetActive(false);
            }

        }
    }
}