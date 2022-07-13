using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Code.Infrastructure;

namespace Assets.Code.UI.Menu
{
    public class Menu : MonoBehaviour
    {
        public delegate void OnSwitchControls();
        public static event OnSwitchControls ToKeyboard;
        public static event OnSwitchControls ToKeyboardPlusMouse;

        public delegate void ChangeGameState();
        public static event ChangeGameState OnResume;
        public static event ChangeGameState RestartGame;

        private const string KeyboardSwitcherText = "Keyboard";
        private const string KeyboardPlusMouseSwitcherText = "Keyboard + Mouse";

        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _controlsMenu;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _buttonPressedAudio;
        [SerializeField] private Toggle _toggleControlSwitcher;
        [SerializeField] private TextMeshProUGUI _toggleText;
        [SerializeField] private GameObject _keyboardDiscription;
        [SerializeField] private GameObject _keyboardPlusMouseDiscription;
        [SerializeField] private GameObject _pauseManager;
        [SerializeField] private GameObject _gameOverMenu;

        private bool _gameIsStarted = false;

        private void Start()
        {
            _resumeButton.interactable = false;
            PauseManager.OnPause += EnablePauseMenu;
            PauseManager.OnResume += DisablePauseMenu;
        }

        private void EnablePauseMenu()
        {
            _mainMenu.SetActive(true);
        }

        public void DisablePauseMenu()
        {
            _mainMenu.SetActive(false);
        }

        public void ResumeButton()
        {
            DisablePauseMenu();

            OnResume?.Invoke();
        }

        public void BackToMainMenu()
        {
            _mainMenu.SetActive(true);
            _controlsMenu.SetActive(false);

            _pauseManager.SetActive(true);
        }

        public void StartNewGame()
        {
            //Снимать с паузы
            _mainMenu.SetActive(false);
            _resumeButton.interactable = true;

            if (_gameIsStarted == false)
            {
                OnResume?.Invoke();
                _gameIsStarted = true;
            }
            else
            {
                OnResume?.Invoke();
                RestartGame?.Invoke();
            }
        }

        public void Restart()
        {
            _gameOverMenu.SetActive(false);
            RestartGame?.Invoke();
        }

        public void OpenControlsMenu()
        {
            //Снимать с паузы
            _mainMenu.SetActive(false);
            _controlsMenu.SetActive(true);

            _pauseManager.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void PlayButtonSound()
        {
            _audioSource.PlayOneShot(_buttonPressedAudio);
        }

        public void SwitchControl()
        {
            if (_toggleControlSwitcher.isOn)
            {
                //KeyboardPlusMouse
                SwitchToKeyboardPlusMouse();
            }
            else
            {
                //Keyboard
                SwitchToKeyboard();
            }
        }

        private void SwitchToKeyboard()
        {
            _keyboardDiscription.SetActive(true);
            _keyboardPlusMouseDiscription.SetActive(false);
            _toggleText.SetText(KeyboardSwitcherText);
            ToKeyboard?.Invoke();
        }

        private void SwitchToKeyboardPlusMouse()
        {
            _keyboardDiscription.SetActive(false);
            _keyboardPlusMouseDiscription.SetActive(true);
            _toggleText.SetText(KeyboardPlusMouseSwitcherText);
            ToKeyboardPlusMouse?.Invoke();
        }
    }
}