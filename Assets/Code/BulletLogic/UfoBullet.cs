using Assets.Code.Interfaces;
using Assets.Code.PlayerLogic.Control;
using Assets.Code.Services;
using UnityEngine;

namespace Assets.Code.BulletLogic
{
    public class UfoBullet : MonoBehaviour
    {
        private float _speed;
        private float _maxFlightDistance;
        private float _totalTravelledDistance;
        private Vector2 _lastPosition;

        private void Start()
        {
            _speed = FindObjectOfType<PlayerMovementSettings>().MaxSpeed;
            GetComponent<BulletOffScreenReturn>().Returned += UpdateLastPosition;
        }

        private void OnEnable()
        {
            _totalTravelledDistance = 0;
            _lastPosition = transform.position;
            _maxFlightDistance = ScreenService._screenWidth;
        }

        private void Update()
        {
            UpdateTravelledDistance();
        }

        private void FixedUpdate()
        {
            transform.Translate(0, _speed * Time.deltaTime, 0, Space.Self);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.transform.TryGetComponent<IDamagable>(out IDamagable asteroid);

            if (asteroid == null)
                return;

            asteroid.ApplyDamage();
            this.gameObject.SetActive(false);
        }

        private void UpdateTravelledDistance()
        {
            var currentPosition = transform.position;

            _totalTravelledDistance += Vector2.Distance(currentPosition, _lastPosition);

            if (_totalTravelledDistance >= _maxFlightDistance)
                this.gameObject.SetActive(false);

            _lastPosition = currentPosition;
        }

        private void UpdateLastPosition()
        {
            _lastPosition = transform.position;
        }
    }
}