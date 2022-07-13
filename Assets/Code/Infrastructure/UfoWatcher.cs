using Assets.Code.UI.Menu;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class UfoWatcher
    {
        private List<GameObject> _pooledUfo;
        private WaveManager _waveManager;

        public void GetAllDependencies(UfoPooling ufoPooling, WaveManager waveManager)
        {
            _waveManager = waveManager;

            GetPooledUfo(ufoPooling);
            Menu.RestartGame += DisableAllUfos;
        }

        public bool CheckUfoCount()
        {
            Debug.Log("Ufo Counted");

            for (int i = 0; i < _pooledUfo.Count; i++)
            {
                if (_pooledUfo[i].activeSelf)
                    return true;
            }

            _waveManager.UfoDestroyed = true;
            _waveManager.CheckWave();
            return false;
        }

        private void GetPooledUfo(UfoPooling ufoPooling)
        {
            _pooledUfo = new List<GameObject>();

            _pooledUfo = ufoPooling.pooledUfo;
        }

        private void DisableAllUfos()
        {
            for (int i = 0; i < _pooledUfo.Count; i++)
            {
                _pooledUfo[i].SetActive(false);
            }
        }
    }
}
