using System;
using System.Collections;
using UnityEngine;

namespace Assets.Code.PlayerLogic
{
    public class PlayerSpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _playerRenderer;
        [SerializeField] private float _countOfFadingInSecond;
        [SerializeField] private float _totalTimeOfImmortaltality;
        [SerializeField] private EdgeCollider2D _edgeCollider2D;

        private float _timeToFadeInSeconds => (1f / _countOfFadingInSecond) * 0.5f;
        private float _currentTimeOfImmortality;
        private bool _readyToFade = true;
        private bool _faded = false;
        private Color tempColor;

        private void OnEnable()
        {
            StartImmortal();
        }

        private void Update()
        {
            _currentTimeOfImmortality += Time.deltaTime;

            if (_readyToFade)
            {
                if (_faded == false)
                    FadeOut();
                else
                    FadeIn();
            }
        }

        private void FadeOut()
        {
            tempColor.a -= (1f * Time.deltaTime) / _timeToFadeInSeconds;
            _playerRenderer.color = tempColor;

            if (tempColor.a <= 0)
                _faded = true;
        }

        private void FadeIn()
        {
            tempColor.a += (1f * Time.deltaTime) / _timeToFadeInSeconds;
            _playerRenderer.color = tempColor;

            if (tempColor.a >= 1f)
                _faded = false;

            if (_currentTimeOfImmortality >= _totalTimeOfImmortaltality)
            {
                _readyToFade = false;
                tempColor.a = 1f;
                _playerRenderer.color = tempColor;
                FinishImmortal();
            }
        }

        private void FinishImmortal()
        {
            _currentTimeOfImmortality = 0f;
            _edgeCollider2D.enabled = true;
            this.enabled = false;
        }

        private void StartImmortal()
        {
            tempColor = _playerRenderer.color;
            _readyToFade = true;
            _edgeCollider2D.enabled = false;
        }
    }

}