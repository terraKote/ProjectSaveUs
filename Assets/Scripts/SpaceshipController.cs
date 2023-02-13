using UnityEngine;

namespace SaveUs
{
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] private float _alignedMoveSpeed = 10f;
        [SerializeField] private float _unalignedMoveSpeed = 5f;
        [SerializeField] private bool _alignSpeedWithDirection = true;
        [SerializeField] private Rigidbody2D _rigidbody;

        private void RotateSpaceshipInDirection(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void PerformMovement(Vector2 moveVector, Vector2 lookDirection)
        {
            var moveSpeed = _alignedMoveSpeed;

            if (_alignSpeedWithDirection)
            {
                var dot = (Vector2.Dot(moveVector.normalized, lookDirection.normalized) + 1f) * 0.5f;
                moveSpeed = Mathf.Lerp(_unalignedMoveSpeed, _alignedMoveSpeed, dot);
            }

            _rigidbody.velocity = moveVector * moveSpeed;
        }

        public void Move(Vector2 moveVector, Vector2 lookDirection)
        {
            PerformMovement(moveVector, lookDirection);
            RotateSpaceshipInDirection(lookDirection);
        }
    }
}