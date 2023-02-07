using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const float MaxInputMagnitudeLength = 1f;

    [SerializeField] private float _alignedMoveSpeed = 10f;
    [SerializeField] private float _unalignedMoveSpeed = 5f;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Camera _camera;

    private Vector2 _moveVector;
    private Vector2 _lookVector;
    private float _moveSpeed;

    private void Update()
    {
        ReadMovementInput();
        ReadMouseInput();

        var dot = Vector2.Dot(_moveVector.normalized, _lookVector.normalized);
        _moveSpeed = Mathf.Lerp(_unalignedMoveSpeed, _alignedMoveSpeed, dot);

        var pos = transform.position;
        Debug.DrawRay(pos, _moveVector.normalized, Color.green);
        Debug.DrawRay(pos, _lookVector.normalized, Color.blue);
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    private void LateUpdate()
    {
        var angle = Mathf.Atan2(_lookVector.y, _lookVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ReadMouseInput()
    {
        _lookVector = Vector3.Normalize(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }

    private void ReadMovementInput()
    {
        _moveVector = Vector2.ClampMagnitude(new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)),
            MaxInputMagnitudeLength);
    }

    private void PerformMovement()
    {
        _rigidbody.velocity = _moveVector * _moveSpeed;
    }
}