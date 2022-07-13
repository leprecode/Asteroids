using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidMover : MonoBehaviour
    {
        private float _currentSpeed;

        public void ChooseDirection(float lastDirectionOfParent)
        {
            var minInclusive = lastDirectionOfParent - 45f;
            var maxInclusive = lastDirectionOfParent + 45f;
            
            var newRandomDirection = UnityEngine.Random.Range(minInclusive, maxInclusive);
            transform.Rotate(0,0,newRandomDirection);
        }

        public void ApplySpeed(float newSpeed) => _currentSpeed = newSpeed;

        private void FixedUpdate()
        {
            transform.Translate(0, _currentSpeed * Time.fixedDeltaTime, 0, Space.Self);
        }
    }
}