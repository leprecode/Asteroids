using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerKeyboardPlusMouseControl : MonoBehaviour
    {
        private void Awake()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("KeyboardAndMousePlay");
            }
        }
    }
}