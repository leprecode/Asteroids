using Assets.Code.UI.Menu;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class UfoBulletPooler
    {
        private GameObject _bulletPrefab;
        private Transform _bulletsPoolParent;
        private const int _countOfBullets = 15;
        private List<GameObject> _pooledBullets;
        public UfoBulletPooler(Transform bulletsPoolParent, GameObject bulletPrefab)
        {
            _bulletsPoolParent = bulletsPoolParent;
            _bulletPrefab = bulletPrefab;
            _pooledBullets = new List<GameObject>();

            Menu.RestartGame += DisableAllBullets;

            CreateBullets();
        }

        private void CreateBullets()
        {
            for (int i = 0; i < _countOfBullets; i++)
            {
                var newBullet = Object.Instantiate(_bulletPrefab, _bulletsPoolParent.position, Quaternion.identity, _bulletsPoolParent.transform);
                _pooledBullets.Add(newBullet);
                newBullet.SetActive(false);
            }
        }

        private void DisableAllBullets()
        {
            for (int i = 0; i < _pooledBullets.Count; i++)
            {
                _pooledBullets[i].SetActive(false);
            }
        }
    }
}