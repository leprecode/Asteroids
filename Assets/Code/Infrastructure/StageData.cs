﻿using UnityEngine;

namespace Assets.Code.Infrastructure
{
    [CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageDataScriptableObject", order = 1)]
    public class StageData : ScriptableObject
    {
        //Hide later
        [SerializeField] private GameObject _bigAsteroidPrefab;

        //DontHide
        [SerializeField] private int startCountOfAsteroids;

        public GameObject BigAsteroidPrefab { get => _bigAsteroidPrefab; }
        public int StartCountOfAsteroids { get; private set; }
    }
}