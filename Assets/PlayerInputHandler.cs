using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")] 
    [SerializeField] private string move = "Move";
    [SerializeField] private string fishing = "Fishing";

    private InputAction moveAction;
    private InputAction fishingAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 FishingInput { get; private set; }

    public static PlayerInputHandler Instance { get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        fishingAction = playerControls.FindActionMap(actionMapName).FindAction(fishing);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        fishingAction.performed += context => FishingInput = context.ReadValue<Vector2>();
        fishingAction.canceled += context => FishingInput = Vector2.zero;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        fishingAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        fishingAction.Disable();
    }
}
