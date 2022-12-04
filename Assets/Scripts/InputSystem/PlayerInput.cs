using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string keyUp = "w";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyDown = "s";

    public float Dup;
    public float Dright;
    private void Start()
    {
        
    }

    private void Update()
    {
        Dup = (Input.GetKey(keyUp)? 1.0f:0) - (Input.GetKey(keyDown)? 1.0f : 0);
        Dright = (Input.GetKey(keyLeft)? 1.0f:0) - (Input.GetKey(keyRight)? 1.0f : 0);
    }
}
