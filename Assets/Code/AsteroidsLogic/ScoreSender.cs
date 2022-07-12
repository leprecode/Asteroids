using Assets.Code.Infrastructure;
using System.Collections;
using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class ScoreSender : MonoBehaviour
    {
        [SerializeField] private TypesOfEnemy _typeOfEnemy;
        private Score _score;
        private void Start()
        {
            _score = FindObjectOfType<GameEntryPoint>().Game.Score;
        }

        private void OnDisable()
        {
            _score.AddScore(_typeOfEnemy);
            Debug.Log($"Type {_typeOfEnemy} disabled");
        }
    }
}