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
            Debug.Log("BulletStart" + this.gameObject.name);

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

            Debug.Log("_screenWidth" + _maxFlightDistance);
        }

        private void Update()
        {
            var currentPosition = transform.position;

            _totalTravelledDistance += Vector2.Distance(currentPosition, _lastPosition);
            
            if (_totalTravelledDistance >= _maxFlightDistance)
            {
                Debug.Log("Bullet Flight Enough");

                this.gameObject.SetActive(false);
            }

            _lastPosition = currentPosition; 
        }

        private void UpdateLastPosition()
        {
            _lastPosition = transform.position;

            Debug.Log("Event");
        }
    }
}