using Assets.Code.UI.Menu;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class PauseManager : MonoBehaviour
    {
        public delegate void OnPauseOrResume();
        public static event OnPauseOrResume OnPause;
        public static event OnPauseOrResume OnResume;

        private GameStates _currentState;

        private void Start()
        {
            Menu.OnResume += ToResume;

            ToPause();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_currentState == GameStates.onPlay)
                    ToPause();
                else
                    ToResume();
            }
        }

        private void ToResume()
        {
            Time.timeScale = 1f;
            _currentState = GameStates.onPlay;
            OnResume?.Invoke();
        }

        private void ToPause()
        {
            Time.timeScale = 0f;
            _currentState = GameStates.onPause;
            OnPause?.Invoke();
        }
    }

    enum GameStates
    {
        onPause,
        onPlay
    }
}