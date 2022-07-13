using Assets.Code.UI.Menu;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class Score
    {
        public delegate void OnValueChanged(int value);
        public static event OnValueChanged ValueIncreased;

        private const int PriceForSmallAsteroid = 100;
        private const int PriceForMidAsteroid = 50;
        private const int PriceForBigAsteroid = 20;
        private const int PriceForUfo = 200;
        private int _score = 0;

        public Score()
        {
            Menu.RestartGame += ResetScore;
        }

        public void AddScore(TypesOfEnemy type)
        {
            switch (type)
            {
                case TypesOfEnemy.SmallAsteroid:
                    _score += PriceForSmallAsteroid;
                    break;
                case TypesOfEnemy.MidAsteroid:
                    _score += PriceForMidAsteroid;
                    break;
                case TypesOfEnemy.BigAsteroid:
                    _score += PriceForBigAsteroid;
                    break;
                case TypesOfEnemy.Ufo:
                    _score += PriceForUfo;
                    break;
                default:
                    Debug.LogError("Wrong type");
                    break;
            }

            ValueIncreased?.Invoke(_score);
        }

        private void ResetScore()
        {
            _score = 0;
            ValueIncreased?.Invoke(_score);
        }
    }
}