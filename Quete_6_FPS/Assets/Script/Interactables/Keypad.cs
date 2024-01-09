using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Keypad : Interactable
{
    [SerializeField] private GameObject _door;
    private bool _doorOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this function is where we will design our interaction using code 
    protected override void Interact()
    {
        _doorOpen = !_doorOpen;
        _door.GetComponent<Animator>().SetBool("IsOpen", _doorOpen);
        
        Debug.Log("Interacted with " + gameObject.name);
    }
}