using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerKeyboardControl : MonoBehaviour
    {
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _boost;
        [SerializeField] private float _turningSpeed;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Shooting _shooting;
        float rotation;
        float forwardDirection;

        Quaternion newRotation;

        void Update()
        {
            if (Input.GetKey(KeyCode.W))
                forwardDirection = 1f;
            else
                forwardDirection = 0f;

            rotation = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                _shooting.Shoot();
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.AddRelativeForce(Vector2.up * forwardDirection * _boost * Time.fixedDeltaTime);
            //  transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.fixedDeltaTime * _turningSpeed);
            transform.Rotate(0, 0, -rotation * _turningSpeed * Time.fixedDeltaTime);
            //_rigidbody.AddTorque(-rotation * _turningSpeed);

            /*   Debug.Log("Velocity" + _rigidbody.velocity);

               if (_rigidbody.velocity.magnitude >= _maxVelocity)
                   _rigidbody.velocity = _rigidbody.velocity.normalized * _maxVelocity;

               Debug.Log("Velocity after rework" + _rigidbody.velocity.normalized);*/
        }



    }
}
