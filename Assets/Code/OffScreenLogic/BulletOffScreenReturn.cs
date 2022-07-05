using Assets.Code.BulletLogic;
using UnityEngine;

namespace Assets.Code.OffScreenLogic
{
    public class BulletOffScreenReturn : MonoBehaviour
    {
        public delegate void OnReturn();
        public event OnReturn Returned;

        [SerializeField] private Renderer _spriteRenderer;
        [SerializeField] private Bullet _bullet;


        private void OnBecameInvisible()
        {
            var newWorldPosition = transform.position;

            newWorldPosition.y = -newWorldPosition.y;
            newWorldPosition.x = -newWorldPosition.x;

            transform.position = newWorldPosition;

            Returned?.Invoke();
        }

    }
}


