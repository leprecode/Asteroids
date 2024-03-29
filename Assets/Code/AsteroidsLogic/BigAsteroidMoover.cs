﻿using System.Collections;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public partial class BigAsteroidMoover : MonoBehaviour
    {
        private const int _maxRotationAngleToMove = 360;
        
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        private float _currentSpeed;
        
        private void Start()
        {
            ChooseDirection();

            ChooseSpeed();
        }

        private void FixedUpdate()
        {
            transform.Translate(0, _currentSpeed * Time.fixedDeltaTime, 0, Space.Self);
        }

        private void ChooseSpeed()
        {
            var RandomSpeed = Random.Range(_minSpeed, _maxSpeed);
            _currentSpeed = Mathf.Round(RandomSpeed * 10) / 10;
        }

        private void ChooseDirection()
        {
            var RandomAngleToMove = Random.Range(0, _maxRotationAngleToMove + 1);
            transform.Rotate(0, 0, RandomAngleToMove);
        }
    }
}