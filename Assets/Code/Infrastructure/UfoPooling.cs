using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class UfoPooling
    {
        public List<GameObject> pooledUfo { get; private set; }
        
        private readonly Transform _parentOfUfo;
        private readonly GameObject _ufoPrefab;
        private const int _countOfUfo = 5;
        private Vector2 _startPosition = new Vector2(0, 0);

        public UfoPooling(Transform parentOfUfo, GameObject ufoPrefab)
        {
            _parentOfUfo = parentOfUfo;
            _ufoPrefab = ufoPrefab;

            Pooling(_countOfUfo);
        }

        private void Pooling(int countToCreate)
        {
            pooledUfo = new List<GameObject>();

            for (int i = 0; i < countToCreate; i++)
            {
                GameObject newAsteroid = Object.Instantiate(_ufoPrefab, _startPosition, Quaternion.identity, _parentOfUfo);
                newAsteroid.SetActive(false);

                pooledUfo.Add(newAsteroid);
            }
        }
    }
}