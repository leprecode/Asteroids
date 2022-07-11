using UnityEngine;

namespace Assets.Code.PlayerLogic.Control
{
    public class PlayerKeyboardControl : MonoBehaviour
    {
        private void Awake()
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("KeyboardPlay");
            }
        }
    }
}