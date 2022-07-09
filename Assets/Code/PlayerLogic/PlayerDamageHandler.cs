using Assets.Code.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerDamageHandler : MonoBehaviour, IDamagable
    {
        public delegate void OnDamage();
        public static event OnDamage OnTakeDamage;

        [SerializeField] private GameObject _playerShip;
        [SerializeField] private GameObject _brokenShip;

        private float timeToResetPlayer = 0.5f;
        public float TimeToResetPlayerAfterDamage { get => timeToResetPlayer; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable);

            if (damagable == null)
                return;

            damagable.ApplyDamage();
            ApplyDamage();
        }


        public void ApplyDamage()
        {
            // BrokeShip();
            //Invoke("RepaierShip", timeToResetPlayer);
            
            OnTakeDamage?.Invoke();
        }

        private void BrokeShip()
        {
            _playerShip.SetActive(false);
            _brokenShip.SetActive(true);
        }

        private void RepairShip()
        {
            _playerShip.SetActive(true);
            _brokenShip.SetActive(false);
        }
    }

    public class PlayerSpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerDamageHandler _playerDamageHandler;
        [SerializeField] private SpriteRenderer _playerRenderer;


        private void FadeInAndOut()
        {
            
        }
    }

}