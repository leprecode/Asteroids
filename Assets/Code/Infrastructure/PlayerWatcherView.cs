using Assets.Code.PlayerLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class PlayerWatcherView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _lifesIcons = new List<GameObject>();

        private void Awake()
        {
            PlayerDamageHandler.OnTakeDamage += DecreaseLifeCount;
        }

        private void DecreaseLifeCount()
        {
            for (int i = 0; i < _lifesIcons.Count; i++)
            {
                if (_lifesIcons[i].activeSelf)
                {
                    Debug.Log("DisableIcon");
                    _lifesIcons[i].SetActive(false);
                    return;
                }
            }    
        }
    }
}