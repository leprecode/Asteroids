using Assets.Code.Infrastructure;
using Assets.Code.UI.Menu;
using UnityEngine;

namespace Assets.Code.PlayerLogic.Control
{
    public class PlayerKeyboardPlusMouseControl : MonoBehaviour
    {
        [HideInInspector] public float _maxSpeed;
        [HideInInspector] public float _acceleration;
        [HideInInspector] public float _rotationSpeed;
        [HideInInspector] public float _brakingSpeed;

        [SerializeField] private Shooting _shooting;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TrailRenderer _trail;

        private float _timeToEnableTrailAfterDamage = 1.0f;
        private Quaternion _targetAngleRotation;
        private Camera _camera;
        private bool _isBoosted = false;
        private float _currentSpeed;
        private bool _allowToRotate = true;
        private Vector2 _worldCenter = new Vector2(0f, 0f);
        private Quaternion _startRotation = new Quaternion(0f, 0f, 0f, 1f);
        private bool _inPause = false;

        private void Start()
        {
            _camera = Camera.main;

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

            float angle = MouseAngleCalculate();
            _targetAngleRotation = Quaternion.Euler(0, 0, angle);

            CheckShootingInput();
            CheckMovementInput();

            AccelerationAndBraking();
            CheckCurrentSpeed();
            CheckAudio();
        }

        private void FixedUpdate()
        {
            if (_allowToRotate && _targetAngleRotation.z != 0)
            {
                var timeOfInterpolation = (_rotationSpeed * Time.fixedDeltaTime) * Time.fixedDeltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetAngleRotation, timeOfInterpolation);
            }

            transform.Translate(0, _currentSpeed * Time.fixedDeltaTime, 0, Space.Self);
        }

        private void PrepareForPause()
        {
            _inPause = true;
            Debug.Log("PlayerInPause");
        }

        private void ResumeGame()
        {
            _inPause = false;
            Cursor.lockState = CursorLockMode.None;
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

        private void CheckShootingInput()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
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

        private float MouseAngleCalculate()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var lookDir = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            var distanceBetween = Vector2.Distance(transform.position, mousePosition);

            if (distanceBetween < 1f)
                return 0;
            else
                return angle;
        }

        private void CheckMovementInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetMouseButton(1))
                _isBoosted = true;
            else
                _isBoosted = false;
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
            _targetAngleRotation.z = 0;
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