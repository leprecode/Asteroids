using UnityEngine;

namespace Assets.Code.AsteroidsLogic
{
    public class AsteroidMover : MonoBehaviour
    {
        [SerializeField] private float MinAngleOfNewDirection = 45f;
        [SerializeField] private float MaxAngleOfNewDirection = 45f;
        private float _currentSpeed;

        public void ChooseDirection(float lastDirectionOfParent)
        {
            var minInclusive = lastDirectionOfParent - MinAngleOfNewDirection;
            var maxInclusive = lastDirectionOfParent + MaxAngleOfNewDirection;
            
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