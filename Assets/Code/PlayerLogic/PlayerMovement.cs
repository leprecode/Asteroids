using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _brakingSpeed = 1.4f;
        private float _timeToEnableTrailAfterDamage = 1.0f;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Shooting _shooting;
        [SerializeField] private TrailRenderer _trail;

        private bool _isBoosted = false;
        private bool _allowToRotate = true;
        private float _rotation;
        private float _currentSpeed;

        private Vector2 _worldCenter = new Vector2(0f,0f);
        private Quaternion _startRotation = new Quaternion(0f,0f,0f,1f);

        private void Start()
        {
            PlayerDamageHandler.OnTakeDamage += ResetPlayerAfterDamage;

            PlayerOffScreenReturn.OnInvisible += PrepareToInvisible;
            PlayerOffScreenReturn.OnVisible += PrepareToVisible;
        }

        private void Update()
        {
            CheckInput();

            AccelerationAndBraking();

            CheckCurrentSpeed();
        }

        private void FixedUpdate()
        {
            if (_allowToRotate)
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

        private void ResetPlayerAfterDamage()
        {
            DisableTrailEmmiting();

            transform.position = _worldCenter;
            transform.rotation = _startRotation;

            _allowToRotate = true;
            _isBoosted = false;
            _currentSpeed = 0f;

            Invoke("EnableTrailEmmiting", _timeToEnableTrailAfterDamage);
        }

        private void PrepareToInvisible()
        {
            _trail.emitting = false;
            _allowToRotate = false;
        }        
        
        private void PrepareToVisible()
        {
            _trail.emitting = true;
            _allowToRotate = true;
        }

        private void EnableTrailEmmiting()
        {
            _trail.enabled = true;
        }
        private void DisableTrailEmmiting()
        {
            _trail.enabled = false;
        }
    }
}
