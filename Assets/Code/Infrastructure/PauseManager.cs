using Assets.Code.PlayerLogic;
using Assets.Code.PlayerLogic.Control;
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

        private void Update()
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
            Cursor.lockState = CursorLockMode.Locked;
            OnResume?.Invoke();
        }

        private void ToPause()
        {
            Time.timeScale = 0f;
            _currentState = GameStates.onPause;
            Cursor.lockState = CursorLockMode.None;
            OnPause?.Invoke();
        }   
    }
}