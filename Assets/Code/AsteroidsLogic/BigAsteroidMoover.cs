using System.Collections;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public partial class BigAsteroidMoover : MonoBehaviour
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private const int _maxRotationAngleToMove = 360;
        private float _currentSpeed;

        private void Start()
        {
            ChooseDirection();

            ChooseSpeed();
        }
        private void Update()
        {
            transform.Translate(0, _currentSpeed * Time.deltaTime, 0, Space.Self);
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