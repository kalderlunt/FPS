using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float _xRotation = 0.0f;

    public float xSensivity = 30.0f;
    public float ySensivity = 30.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       //Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        
        //calculate camera rotation for looking up and down
        _xRotation -= (mouseY * Time.deltaTime) * xSensivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f); // camera blocked horizontal
        
        //apply this to our camera transform
        cam.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        
        //rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensivity);
    }
}