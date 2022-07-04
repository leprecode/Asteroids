using Assets.Code.BulletLogic;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Borders
{
    public class BulletDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var bullet = collision.GetComponent<Bullet>();

            if (bullet != null)
                bullet.gameObject.SetActive(false);
        }
    }
}