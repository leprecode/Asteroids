using System.Collections;
using UnityEngine;

namespace Assets.Code.PlayerLogic.Control
{
    public class PlayerMovementSettings : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _brakingSpeed = 1.4f;

        [SerializeField] private PlayerKeyboardControl _keyboardControl;
        [SerializeField] private PlayerKeyboardPlusMouseControl _mouseControl;

        public float MaxSpeed { get => _maxSpeed;}

        private void Start()
        {
            ConfigureKeyvoardControls();

            ConfigureMouseControls();
        }

        private void ConfigureMouseControls()
        {
            _mouseControl._maxSpeed = _maxSpeed;
            _mouseControl._acceleration = _acceleration;
            _mouseControl._rotationSpeed = _rotationSpeed;
            _mouseControl._brakingSpeed = _brakingSpeed;
        }

        private void ConfigureKeyvoardControls()
        {
            _keyboardControl._maxSpeed = _maxSpeed;
            _keyboardControl._acceleration = _acceleration;
            _keyboardControl._rotationSpeed = _rotationSpeed;
            _keyboardControl._brakingSpeed = _brakingSpeed;
        }
    }
}