using Assets.Code.BulletLogic;
using System;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidDamageHandler : MonoBehaviour
    {
        private void OnDisable()
        {
            Debug.Log("Disabled");
            ApplyDamage();
        }

        private void ApplyDamage()
        {
            var isAnyChild = CheckChild();

            if (isAnyChild)
            {
                Debug.Log("Child");
                EnableSmallerAsteroids();
            }

            //this.gameObject.SetActive(false);
        }

        private void EnableSmallerAsteroids()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        private bool CheckChild()
        {
            if (transform.childCount > 0)
                return true;
            else
                return false;
        }
    }
}