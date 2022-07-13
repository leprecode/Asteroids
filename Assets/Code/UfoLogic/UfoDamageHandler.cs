using Assets.Code.Infrastructure;
using Assets.Code.Interfaces;
using UnityEngine;

namespace Assets.Code.UfoLogic
{
    public class UfoDamageHandler : MonoBehaviour, IDamagable
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _explosionAudio;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CircleCollider2D _circleCollider;
        
        private UfoWatcher _ufoWatcher;

        private float _timeToDisable => _explosionAudio.length;

        private void Start()
        {
            _ufoWatcher = FindObjectOfType<GameEntryPoint>().Game.UfoWatcher;
        }

        public void ApplyDamage()
        {
            _audioSource.PlayOneShot(_explosionAudio);

            _spriteRenderer.enabled = false;
            _circleCollider.enabled = false;
            Invoke("Disable", _timeToDisable);   
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable);

            if (damagable == null)
                return;

            damagable.ApplyDamage();
        }

        private void Disable()
        {
            _spriteRenderer.enabled = true;
            _circleCollider.enabled = true;
            this.gameObject.SetActive(false);
            _ufoWatcher.CheckUfoCount();
        }
    }
}