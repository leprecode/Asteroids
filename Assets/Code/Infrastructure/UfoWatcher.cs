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
        }

        private void GetPooledUfo(UfoPooling ufoPooling)
        {
            _pooledUfo = new List<GameObject>();

            _pooledUfo = ufoPooling.pooledUfo;
        }

        public void CheckUfoCount()
        {
            for (int i = 0; i < _pooledUfo.Count; i++)
            {
                if (_pooledUfo[i].activeSelf)
                    return;
            }

            _waveManager.CreateNewUfo();
        }
    }
}
