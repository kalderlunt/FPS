using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.y = input.y;
        _controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
    }

}
