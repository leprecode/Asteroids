using System;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Services
{
    public class ScreenService
    {
        private Camera _camera;

        public static float _screenWidth { get; private set; }
        public  static float _screenHeight { get; private set; }

        public  float _leftPointX { get; private set; }
        public  float _rightPointX { get; private set; }
        public  float _topPointY { get; private set; }
        public  float _bottomPointY { get; private set; }

        public ScreenService(Camera camera)
        {
            _camera = camera;
        }


        private void Awake()
        {

            CalculatePoints();
            CalculateScreenWidth();
            CalculateScreenHeight();
        }

        private void CalculatePoints()
        {
            _leftPointX = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            _rightPointX = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0, 0)).x;
            _topPointY = _camera.ScreenToWorldPoint(new Vector3(0, _camera.pixelHeight, 0)).y;
            _bottomPointY = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        }

        private void CalculateScreenWidth()
        {
            _screenHeight = Vector2.Distance(new Vector2(0, _bottomPointY), new Vector2(0, _topPointY));
        }

        private void CalculateScreenHeight()
        {
            _screenWidth = Vector2.Distance(new Vector2(_leftPointX, 0), new Vector2(_rightPointX, 0));
        }

    }
}