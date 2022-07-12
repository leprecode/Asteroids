using Assets.Code.UI.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool isBoosted = false;
        public float rotation;

        private const float _brakingSpeed = 1.4f;
        private const float _timeToEnableTrailAfterDamage = 1.0f;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _thrustAudio;

        private bool _allowToRotate = true;
        private float _currentSpeed;

        private Vector2 _worldCenter = new Vector2(0f, 0f);
        private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 1f);

        private void Start()
        {
            PlayerDamageHandler.OnTakeDamage += ResetPlayerAfterDamage;

            PlayerOffScreenReturn.OnInvisible += PrepareToInvisible;
            PlayerOffScreenReturn.OnVisible += PrepareToVisible;

            Menu.RestartGame += ResetPlayerAfterDamage;
        }

        private void Update()
        {
            AccelerationAndBraking();

            CheckCurrentSpeed();
        }

        private void FixedUpdate()
        {
            if (_allowToRotate)
                transform.Rotate(0, 0, -rotation * _rotationSpeed * Time.fixedDeltaTime);

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
            if (isBoosted)
                _currentSpeed += _acceleration * Time.deltaTime;
            else
                _currentSpeed -= _brakingSpeed * Time.deltaTime;
        }

        private void ResetPlayerAfterDamage()
        {
            DisableTrailEmmiting();

            transform.position = _worldCenter;
            transform.rotation = _startRotation;

            _allowToRotate = true;
            isBoosted = false;
            _currentSpeed = 0f;

            Invoke("EnableTrailEmmiting", _timeToEnableTrailAfterDamage);
        }

        private void PrepareToInvisible()
        {
            _trail.emitting = false;
            _allowToRotate = false;
            rotation = 0;
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
