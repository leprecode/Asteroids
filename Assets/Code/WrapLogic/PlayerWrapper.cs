using Assets.Code.PlayerLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrapper : MonoBehaviour
{
    public delegate void ChangeVisible();
    public static event ChangeVisible OnInvisible;
    public static event ChangeVisible OnVisible;

    [SerializeField] private ScreenBounds _screenBounds;

    private bool _isAfterInvisible = false;

    void Update() => Wrapping();

    private void Wrapping()
    {
        if (_screenBounds.AmIOutOfBounds(transform.localPosition))
        {
            OnInvisible?.Invoke();

            Vector2 newPosition = _screenBounds.CalculateWrappedPosition(transform.localPosition);
            transform.position = newPosition;

            _isAfterInvisible = true;
        }
        else if (_isAfterInvisible)
        {
            OnVisible?.Invoke();
            _isAfterInvisible = false;
        }   
    }
}
