using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _playerInput;
    private PlayerInputs.OnFootActions _onFoot;

    private PlayerMotor _motor;
    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerInputs();
        _onFoot = _playerInput.OnFoot;

        _motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // tell the playermotor to move using the value from our movement action.
        _motor.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }
    
    private void OnDisable()
    {
        _onFoot.Disable();
    }
}
