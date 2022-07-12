using Assets.Code.PlayerLogic;
using Assets.Code.UI.Menu;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class PlayerWatcher
    {
        public delegate void PLayerEvent();
        public static event PLayerEvent PlayerDestroyed;
        public static event PLayerEvent PlayerValuesChanged;

        private const int _maxCountOfPlayersLife = 5;
        private int _currentPlayersLifeCount;

        public PlayerWatcher()
        {
            _currentPlayersLifeCount = _maxCountOfPlayersLife;
            PlayerDamageHandler.OnTakeDamage += DecreaseLifeCount;

            Menu.RestartGame += RestartGame;
        }

        private void DecreaseLifeCount()
        {
            _currentPlayersLifeCount -= 1;

            Debug.Log("DecreaseRealLifes");

            CheckLifeCount();
        }

        private void CheckLifeCount()
        {
            PlayerValuesChanged?.Invoke();

            if (_currentPlayersLifeCount == 0)
                PlayerDestroyed?.Invoke();
        }

        private void RestartGame()
        {
            _currentPlayersLifeCount = _maxCountOfPlayersLife;
        }
    }
}