using Assets.Code.PlayerLogic;
using Assets.Code.UI.Menu;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class PlayerWatcherView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _lifesIcons = new List<GameObject>();

        private void Awake()
        {
            PlayerWatcher.PlayerValuesChanged += DecreaseLifeCount;
            Menu.RestartGame += RestoreLifes;
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

        private void RestoreLifes()
        {
            for (int i = 0; i < _lifesIcons.Count; i++)
            {
                _lifesIcons[i].SetActive(true);
            }
        }
    }
}