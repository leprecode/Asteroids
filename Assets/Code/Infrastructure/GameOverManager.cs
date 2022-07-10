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

        private void FinishGame() => _gameOverMenu.SetActive(true);
    }
}