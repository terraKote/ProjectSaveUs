using System;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI _controlStyleLabel;

        private bool _useRelativeControls = true;

        private void Update()
        {
            var moveInput = ValidateControlStyle(ReadMovementInput());
            var look = ReadMouseInput();
            _controller.Move(Vector2.ClampMagnitude(moveInput, MaxInputMagnitudeLength), look);
        }

        private Vector2 ValidateControlStyle(Vector2 moveInput)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                _useRelativeControls = !_useRelativeControls;
            }

            if (_useRelativeControls)
            {
                moveInput = transform.TransformDirection(new Vector3(moveInput.y, moveInput.x));
            }

            _controlStyleLabel.text = _useRelativeControls ? "Control Style: Relative" : "Control Style: Absolute";

            return moveInput;
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