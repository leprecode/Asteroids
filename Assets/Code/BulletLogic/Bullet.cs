using Assets.Code.AsteroidsLogic;
using Assets.Code.OffScreenLogic;
using Assets.Code.Services;
using System.Collections;
using UnityEngine;

namespace Assets.Code.BulletLogic
{
    public class Bullet : MonoBehaviour
    {
        private float _maxFlightDistance;
        private float _totalTravelledDistance;

        private Vector2 _lastPosition;

        private void Start()
        {
            GetComponent<BulletOffScreenReturn>().Returned += UpdateLastPosition;
        }

        private void OnDisable()
        {
            _totalTravelledDistance = 0;
        }

        private void OnEnable()
        {
            _lastPosition = transform.position;
            _maxFlightDistance = ScreenService._screenWidth;
        }

        private void Update()
        {
            var currentPosition = transform.position;

            _totalTravelledDistance += Vector2.Distance(currentPosition, _lastPosition);
            
            if (_totalTravelledDistance >= _maxFlightDistance)
            {
                this.gameObject.SetActive(false);
            }

            _lastPosition = currentPosition; 
        }

        private void UpdateLastPosition()
        {
            _lastPosition = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.transform.TryGetComponent<AsteroidDamageHandler>(out AsteroidDamageHandler asteroid);

            if (asteroid != null)
            {
                Debug.Log("Its Asteroid");
                collision.gameObject.SetActive(false);
            }
                Debug.Log("Bullet Collision");
            this.gameObject.SetActive(false);
        }
    }
}