using System.Collections;
using UnityEngine;

namespace Assets.Code.Services
{
    public class ScreenService : MonoBehaviour
    {
        private Camera _camera;
        public static float _screenWidth { get; private set; }

        private void Start()
        {
            CalculateScreenWidth();
        }

        private void CalculateScreenWidth()
        {
            _camera = Camera.main;
            _screenWidth = Vector2.Distance(_camera.ViewportToWorldPoint(new Vector3(0f, 0.5f, transform.position.z)), _camera.ViewportToWorldPoint(new Vector3(1f, 0.5f, transform.position.z)));
        }
    }
}