using UnityEngine;

namespace Assets.Code.PlayerLogic.Control
{
    public class PlayerKeyboardControl : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Shooting _shooting;

        private void Update()
        {
            CheckInput();
        }
        private void CheckInput()
        {
            if (Input.GetKey(KeyCode.W))
                _playerMovement.isBoosted = true;
            else
                _playerMovement.isBoosted = false;

            _playerMovement.rotation = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shooting.Shoot();
            }
        }
    }
}