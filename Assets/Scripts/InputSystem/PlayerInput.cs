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

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

    public float smoothTime = 0.2f;

    public bool inputEnable = true;
    private void Start()
    {
        
    }

    private void Update()
    {
        targetDup = (Input.GetKey(keyUp)? 1.0f:0) - (Input.GetKey(keyDown)? 1.0f : 0);
        targetDright = (Input.GetKey(keyLeft)? 1.0f:0) - (Input.GetKey(keyRight)? 1.0f : 0);

        if (inputEnable == false)
        {
            targetDup = 0f;
            targetDright = 0f;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, smoothTime);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, smoothTime);
    }
}
