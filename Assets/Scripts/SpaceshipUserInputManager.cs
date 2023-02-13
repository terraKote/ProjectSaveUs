using System;
using UnityEngine;

namespace SaveUs
{
    public class SpaceshipUserInputManager : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const float MaxInputMagnitudeLength = 1f;

        [SerializeField] private SpaceshipController _controller;
        [SerializeField] private Camera _camera;

        private void Update()
        {
            var input = ReadMovementInput();
            var move = transform.TransformDirection(new Vector3(input.y, input.x));
            var look = ReadMouseInput();
            _controller.Move(Vector2.ClampMagnitude(move, MaxInputMagnitudeLength), look);
        }

        private Vector2 ReadMouseInput()
        {
            return Vector3.Normalize(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }

        private Vector2 ReadMovementInput()
        {
            return new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
        }
    }
}