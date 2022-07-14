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

        private void Start()
        {
            Menu.OnResume += ToResume;

            ToPause();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && (Game.gameState != GameStates.GamOver && Game._isGameStarted))
            {
                if (Game.gameState == GameStates.onPause)
                    ToResume();
                else
                    ToPause();    
            }
        }

        private void ToResume()
        {
            Game.gameState = GameStates.onPlay;
            Time.timeScale = 1f;
            Game.gameState = GameStates.onPlay;
            Cursor.lockState = CursorLockMode.Locked;
            OnResume?.Invoke();
        }

        private void ToPause()
        {
            Game.gameState = GameStates.onPause;
            Time.timeScale = 0f;
            Game.gameState = GameStates.onPause;
            Cursor.lockState = CursorLockMode.None;
            OnPause?.Invoke();
        }
    }
}