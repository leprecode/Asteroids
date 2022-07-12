using Assets.Code.Infrastructure;
using Assets.Code.Interfaces;
using Assets.Code.UI.Menu;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerDamageHandler : MonoBehaviour, IDamagable
    {
        public delegate void OnDamage();
        public static event OnDamage OnTakeDamage;

        [SerializeField] private PlayerSpawnBehaviour _playerSpawnBehaviour;

        private float timeToResetPlayer = 0.5f;
        public float TimeToResetPlayerAfterDamage { get => timeToResetPlayer; }

        private void Start()
        {
            PlayerWatcher.PlayerDestroyed += DestroyPlayer;
            Menu.RestartGame += RestartPlayer;
        }

        private void RestartPlayer()
        {
            this.gameObject.SetActive(true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable);

            if (damagable == null)
                return;

            damagable.ApplyDamage();
        }

        public void ApplyDamage()
        {
            OnTakeDamage?.Invoke();
            _playerSpawnBehaviour.enabled = true;
        }

        private void DestroyPlayer()
        {
            this.gameObject.SetActive(false);
        }
    }
}