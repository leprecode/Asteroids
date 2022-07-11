using Assets.Code.Infrastructure;
using Assets.Code.Interfaces;
using UnityEngine;

namespace Assets.Code.UfoLogic
{
    public class UfoDamageHandler : MonoBehaviour, IDamagable
    {
        private UfoWatcher _ufoWatcher;

        private void Start()
        {
            _ufoWatcher = FindObjectOfType<GameEntryPoint>().Game.UfoWatcher;
        }

        public void ApplyDamage()
        {
            this.gameObject.SetActive(false);
            _ufoWatcher.CheckUfoCount();
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