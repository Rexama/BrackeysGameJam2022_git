using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5.0f;

    
    public PlayerControls playerControls;
    
    private PlayerSelectManager _playerSelectManager;
    
    private InputAction _move;
    private InputAction _select;
    private InputAction _fire;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerSelectManager = GetComponent<PlayerSelectManager>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        _move = playerControls.Player.Move;
        _move.Enable();
        
        _select = playerControls.Player.Select;
        _select.Enable();
        _select.performed += ctx => Select();
        _select.canceled += ctx => FinishSelect();
        
    }

    private void OnDisable()
    {
        playerControls.Disable();
        _move.Disable();
        _select.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var direction = _move.ReadValue<Vector2>();
        var dirMag = direction.magnitude;
        if(dirMag>0.5f)
        {
            transform.right = direction;
            transform.position += dirMag*transform.right * Time.deltaTime * speed;
        }
        
    }

    private void Select()
    {
        _playerSelectManager.Action();
    }
    
    private void FinishSelect()
    {
        _playerSelectManager.FinishHold();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

}
