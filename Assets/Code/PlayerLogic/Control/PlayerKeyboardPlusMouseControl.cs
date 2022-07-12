using UnityEngine;

namespace Assets.Code.PlayerLogic.Control
{
    public class PlayerKeyboardPlusMouseControl : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private PlayerMovement _playerMovement;
        Quaternion rotation;
        float rel;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("KeyboardAndMousePlay");
            }
/*
            Vector3 mousePosition = Input.mousePosition;
            Vector3 targetPosition = _camera.ScreenToWorldPoint(mousePosition);
            Vector3 relativePos = Input.mousePosition - transform.position;*/

            var direction = Quaternion.LookRotation(Input.mousePosition);
            rel =  direction.y - transform.rotation.y;



            //transform.Rotate(0,0,rel* 500 * Time.deltaTime);

            transform.rotation = new Quaternion(0, 0, rel, Quaternion.identity.w); 
            
        }
    }

}