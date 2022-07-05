using UnityEngine;

namespace Assets.Code.OffScreenLogic
{
    public class OffScreenReturn : MonoBehaviour
    {
        [SerializeField] private Renderer _spriteRenderer;

        private void OnBecameInvisible()
        {
            var newWorldPosition = transform.position;

            newWorldPosition.y = -newWorldPosition.y;
            newWorldPosition.x = -newWorldPosition.x;

            transform.position = newWorldPosition;
        }
    }
}


