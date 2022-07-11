using Assets.Code.Infrastructure;
using Assets.Code.Services;
using System.Collections;
using UnityEngine;

namespace Assets.Code.UfoLogic
{
    public class UfoMover : MonoBehaviour
    {
        private const float _targetTimeToCrossScreen = 10f;
        private float _movementSpeed;
        private int _directionOfMovement;

        private void Start()
        {
            var screenWidth = ScreenService._screenWidth;

            _movementSpeed = screenWidth / _targetTimeToCrossScreen;
            Debug.Log("MovementUfoSpeed" + _movementSpeed);
        }

        private void OnEnable()
        {
            var randomDirection = Random.Range(0f, 100f);

            if (randomDirection <= 50)
                _directionOfMovement = -1;
            else
                _directionOfMovement = +1;
        }

        private void FixedUpdate()
        {
            transform.Translate(_directionOfMovement * _movementSpeed * Time.fixedDeltaTime, 0, 0, Space.Self);
        }
    }
}