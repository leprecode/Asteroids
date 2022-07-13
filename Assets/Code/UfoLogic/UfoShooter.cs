using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.UfoLogic
{
    public class UfoShooter : MonoBehaviour
    {
        private const float _minFireRateFrequency = 2f;
        private const float _maxFireRateFrequency = 5f;
        private const string TagOfPlayer = "Player";

        [SerializeField] private GameObject _bulletPrefab;

        private List<GameObject> _bullets;
        private Transform _player;
        private bool _isFiring = false;
        private float _timer;
        private float _randomNextShoot;
        private Transform _poolOfBullets;

        private void Start()
        {
            _bullets = new List<GameObject>();
            GetBulletsFromPool();
        }

        private void GetBulletsFromPool()
        {
            _poolOfBullets = GameObject.FindGameObjectWithTag("POOL_UFO_BULLETS").transform;

            for (int i = 0; i < _poolOfBullets.childCount; i++)
            {
                _bullets.Add(_poolOfBullets.GetChild(i).gameObject);
            }
        }

        private void OnEnable()
        {
            _player = GameObject.FindWithTag(TagOfPlayer).transform;
        }

        private void Update()
        {
            if (_isFiring == false)
            {
                _randomNextShoot = Random.Range(_minFireRateFrequency,_maxFireRateFrequency);
                _isFiring = true;
            }

            _timer += Time.deltaTime;

            if (_timer >= _randomNextShoot)
            {
                Shoot();
                _timer = 0f;
            }
        }

        private void Shoot()
        {
            if (_player == null)
                return;

            var newBullet = ChooseBullet();

            if (newBullet == null)
                return;

            newBullet.transform.position = transform.position;
            newBullet.SetActive(true);

            var direction = _player.position - newBullet.transform.position;
            newBullet.transform.up = direction;

            _isFiring = false;
        }

        private GameObject ChooseBullet()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].activeSelf == false)
                    return _bullets[i];
            }

            return null;
        }
    }

}