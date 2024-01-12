using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    public float speed = 5.0f;
    public float gravity = 9.8f;
    public float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _controller.isGrounded;
    }

    //receive the inputs for our InputManager.cs ans apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        _controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        _playerVelocity.y += -gravity * Time.deltaTime;
        
        if (_isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -2.0f;
        _controller.Move(_playerVelocity * Time.deltaTime);
        
        //Debug.Log(_playerVelocity.y);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * -gravity);
        }
    }
}