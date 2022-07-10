using Assets.Code.PlayerLogic;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class PlayerWatcher
    {
        public delegate void OnDestroy();
        public static event OnDestroy PlayerDestroyed;

        private const int _maxCountOfPlayersLife = 5;
        private int _currentPlayersLifeCount;

        public PlayerWatcher()
        {
            _currentPlayersLifeCount = _maxCountOfPlayersLife;
            PlayerDamageHandler.OnTakeDamage += DecreaseLifeCount;
        }

        private void DecreaseLifeCount()
        {
            _currentPlayersLifeCount -= 1;

            Debug.Log("DecreaseRealLifes");

            CheckLifeCount();
        }

        private void CheckLifeCount()
        {
            if (_currentPlayersLifeCount == 0)
                PlayerDestroyed?.Invoke();
        }
    }
}