using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    private PlayerInput _controls;
    private CharacterController _characterController;
    private Camera _playerCamera;

    void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();

        _characterController = GetComponent<CharacterController>();
        _controls = GetComponent<PlayerInput>();
        _moveAction = _controls.actions["Move"];
        _escapeAction = _controls.actions["Escape"];

    }


    void Update()
    {
        Escape();
        Move();
        Look();
    }

    private InputAction _escapeAction;

    public void Escape()
    {
        var escapeValue = _escapeAction.ReadValue<float>();
        Debug.Log("escape value: " + escapeValue);

        if (escapeValue == 1)
        {
            ToggleCursor();
        }
    }

    private void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        else if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
    }

    public float LookSpeed = 5;
    private Vector2 _currentLookVector;
    private Vector2 _smoothLookVelocity;
    [SerializeField]
    private float _smoothLookSpeed = 0.2f;
    private void Look()
    {

        var mouseValue = Mouse.current.delta.ReadValue();
        _currentLookVector = Vector2.SmoothDamp(_currentLookVector, mouseValue, ref _smoothLookVelocity, _smoothLookSpeed);
        var deltaLook = _currentLookVector * Time.deltaTime * LookSpeed;

        var horizontalLook = new Vector2(0, deltaLook.x);
        transform.Rotate(horizontalLook);

    }

    public float PlayerSpeed = 5;
    private InputAction _moveAction;
    private Vector2 _currentInputVector = new(0, 0);
    private Vector2 _smoothMoveVelocity;
    [SerializeField]
    private float _smoothInputSpeed = 0.2f;
    private void Move()
    {
        var moveValue = _moveAction.ReadValue<Vector2>();

        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, moveValue, ref _smoothMoveVelocity, _smoothInputSpeed);
        var move = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);
        var deltaMove = move * Time.deltaTime * PlayerSpeed;
        transform.position += transform.TransformDirection(deltaMove);
    }



}
