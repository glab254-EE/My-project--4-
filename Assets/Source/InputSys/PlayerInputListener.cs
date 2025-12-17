using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputListener : MonoBehaviour
{
    public static PlayerInputListener Instance {get; private set;}
    public static Vector2 MovementVector;
    public static Vector3 WorldMousePosition;
    public static bool IsLeftMouseDown;
    private InputSystem_Actions inputActions;
    private Camera currentCamera;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        IsLeftMouseDown = false;
        MovementVector = Vector2.zero;
        WorldMousePosition = Vector3.zero;
        inputActions = new();
        inputActions.Player.Attack.performed += OnMouseButtonAction;
        inputActions.Player.Attack.canceled += OnMouseButtonAction;
        inputActions.Player.Enable();
        currentCamera = Camera.main;
    }
    void OnDestroy()
    {
        inputActions.Player.Disable();
    }
    void Update()
    {
        MovementVector = inputActions.Player.Move.ReadValue<Vector2>();
        if(currentCamera == null)
            currentCamera = Camera.main;
        Vector3 mousePosInWorld = currentCamera.ScreenToWorldPoint(Mouse.current.position.value);
        WorldMousePosition = mousePosInWorld;
    }
    private void OnMouseButtonAction(InputAction.CallbackContext callbackContext)
    {
        IsLeftMouseDown = callbackContext.ReadValueAsButton();
    }
}
