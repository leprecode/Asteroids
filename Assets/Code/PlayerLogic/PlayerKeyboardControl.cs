using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerKeyboardControl : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Shooting _shooting;
        
        private bool _isBoosted = false;
        private float _rotation;
        private float _currentSpeed;
        private float _brakingSpeed => _acceleration * 3;

        void Update()
        {
            CheckInput();

            AccelerationAndBraking();

            CheckCurrentSpeed();
        }

        private void FixedUpdate()
        {
            transform.Rotate(0, 0, -_rotation * _rotationSpeed * Time.fixedDeltaTime);
            transform.Translate(0, _currentSpeed * Time.fixedDeltaTime, 0, Space.Self);
        }

        private void CheckCurrentSpeed()
        {
            if (_currentSpeed > _maxSpeed)
                _currentSpeed = _maxSpeed;
            else if (_currentSpeed < 0)
                _currentSpeed = 0;
        }

        private void AccelerationAndBraking()
        {
            if (_isBoosted)
                _currentSpeed += _acceleration * Time.deltaTime;
            else
                _currentSpeed -= _brakingSpeed * Time.deltaTime;
        }

        private void CheckInput()
        {
            if (Input.GetKey(KeyCode.W))
                _isBoosted = true;
            else
                _isBoosted = false;

            _rotation = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shooting.Shoot();
            }
        }
    }
}
