using Assets.Code.Infrastructure;
using Assets.Code.UI.Menu;
using UnityEngine;

namespace Assets.Code.PlayerLogic.Control
{
    public class PlayerKeyboardControl : MonoBehaviour
    {
        [HideInInspector] public float _maxSpeed;
        [HideInInspector] public float _acceleration;
        [HideInInspector] public float _rotationSpeed;
        [HideInInspector] public float _brakingSpeed;

        [SerializeField] private Shooting _shooting;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TrailRenderer _trail;

        private float _timeToEnableTrailAfterDamage = 1.0f;
        private bool _isBoosted = false;
        private float _currentSpeed;
        private bool _allowToRotate = true;
        private Vector2 _worldCenter = new Vector2(0f, 0f);
        private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 1f);
        private float _rotation;
        private bool _inPause = false;

        private void Start()
        {
            PlayerDamageHandler.OnTakeDamage += ResetPlayerAfterDamage;

            PlayerWrapper.OnInvisible += PrepareToInvisible;
            PlayerWrapper.OnVisible += PrepareToVisible;

            Menu.RestartGame += ResetPlayerAfterDamage;

            PauseManager.OnPause += PrepareForPause;
            PauseManager.OnResume += ResumeGame;
        }

        private void Update()
        {
            if (_inPause)
                return;

            CheckShootingInput();
            CheckInput();
            AccelerationAndBraking();
            CheckCurrentSpeed();
            CheckAudio();
        }

        private void FixedUpdate()
        {
            if (_allowToRotate)
            {
                transform.Rotate(0,0, -_rotation * _rotationSpeed * Time.fixedDeltaTime);
            }

            transform.Translate(0, _currentSpeed * Time.fixedDeltaTime, 0, Space.Self);
        }
        private void CheckCurrentSpeed()
        {
            if (_currentSpeed > _maxSpeed)
                _currentSpeed = _maxSpeed;
            else if (_currentSpeed < 0)
                _currentSpeed = 0;
        }

        private void PrepareForPause()
        {
            _inPause = true;
        }

        private void ResumeGame()
        {
            _inPause = false;

            if (enabled)
                Cursor.lockState = CursorLockMode.Locked;
        }

        private void AccelerationAndBraking()
        {
            if (_isBoosted)
                _currentSpeed += _acceleration * Time.deltaTime;
            else
                _currentSpeed -= _brakingSpeed * Time.deltaTime;
        }

        private void CheckShootingInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _shooting.Shoot();
        }
        private void CheckAudio()
        {
            if (_isBoosted)
            {
                if (!_audioSource.isPlaying)
                    _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
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