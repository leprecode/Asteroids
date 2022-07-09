﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    [CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageDataScriptableObject", order = 1)]
    public class StageData : ScriptableObject
    {
        [SerializeField] private GameObject _bigAsteroidPrefab;
        [SerializeField] private int startCountOfAsteroids;

        public GameObject BigAsteroidPrefab { get => _bigAsteroidPrefab; }
    }
}