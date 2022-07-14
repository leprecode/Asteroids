using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverMenu;

        private void Start()
        {
            _gameOverMenu.SetActive(false);
            PlayerWatcher.PlayerDestroyed += FinishGame;
        }

        private void FinishGame()
        {
            Cursor.lockState = CursorLockMode.None;
            _gameOverMenu.SetActive(true);
            Game.gameState = GameStates.GamOver;
            Time.timeScale = 0f;
        }
    }
}