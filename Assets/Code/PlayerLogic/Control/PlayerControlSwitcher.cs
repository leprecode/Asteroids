using Assets.Code.UI.Menu;
using UnityEngine;

namespace Assets.Code.PlayerLogic.Control
{
    public class PlayerControlSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayerKeyboardControl _keyboardControl;
        [SerializeField] private PlayerKeyboardPlusMouseControl _keyboardPlusMouseControl;

        private void Start()
        {
            Menu.ToKeyboard += SwitchToKeyboard;
            Menu.ToKeyboardPlusMouse += SwitchToKeyboardPlusMouse;

            SwitchToKeyboard();
        }

        private void SwitchToKeyboard()
        {
            Debug.Log("SwitchToKeyboard");
            Debug.Log("ActiveK: " + _keyboardControl.isActiveAndEnabled);
            Debug.Log("ActiveKaM: " + _keyboardPlusMouseControl.isActiveAndEnabled);
            _keyboardControl.enabled = true;
            _keyboardPlusMouseControl.enabled = false;
        }

        private void SwitchToKeyboardPlusMouse()
        {
            Debug.Log("SwitchToKeyboardPlusMouse");
            Debug.Log("ActiveK: " + _keyboardControl.isActiveAndEnabled);
            Debug.Log("ActiveKaM: " + _keyboardPlusMouseControl.isActiveAndEnabled);
            _keyboardControl.enabled = false;
            _keyboardPlusMouseControl.enabled = true;
        }

    }
}