using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerControls playerControls;
    private PlayerSelectManager _playerSelectManager;
    private PlayerInput _playerInput;
    private PlayerMove _playerMove;
    
    //Events
    public delegate void OnBack();
    public static event OnBack OnBackButton;
    
    public InputAction move;
    private InputAction _select;
    private InputAction _fire;
    private InputAction _back;
    private void Awake()
    {
        playerControls = new PlayerControls();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerSelectManager = GetComponent<PlayerSelectManager>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        if (_playerInput.defaultControlScheme == "Keyboard&Mouse")
        {
            move = playerControls.PlayerKeyboard.Move;
            _select = playerControls.PlayerKeyboard.Select;
        }
        else
        {
            move = playerControls.PlayerGamepad.Move;
            _select = playerControls.PlayerGamepad.Select;
            _back = playerControls.PlayerGamepad.Back;
            _back.performed += ctx => OnBackButton?.Invoke();
        }
        move.Enable();
        _select.Enable();
        _select.performed += ctx => Select();
        _select.canceled += ctx => FinishSelect();
        
    }

    private void OnDisable()
    {
        playerControls.Disable();
        move.Disable();
        _select.Disable();
    }

    private void Select()
    {
        _playerSelectManager.Action(_playerMove);
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
