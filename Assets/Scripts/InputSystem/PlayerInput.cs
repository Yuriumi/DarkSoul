using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("==== Key Setting ====")]
    public string keyUp = "w";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyDown = "s";

    public string keyRun;
    public string keyJump;
    public string keyAttack;
    public string keyD;

    public string keyJUp;
    public string keyJLeft;
    public string keyJRight;
    public string keyJDown;

    [Header("==== Output signals ====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;

    // 1.Pressing singnal
    public bool run;
    // 2.trigger once signal
    public bool jump;
    public bool attack;
    // 3.double trigger

    [Header("==== Other ====")]
    public bool inputEnable = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

    public float smoothTime = 0.2f;

    private void Start()
    {

    }

    private void Update()
    {
        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnable == false)
        {
            targetDup = 0f;
            targetDright = 0f;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, smoothTime);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, smoothTime);

        Dmag = Mathf.Clamp01(Mathf.Sqrt((Dup * Dup) + (Dright * Dright)));
        Dvec = Dright * transform.right + Dup * transform.forward;

        run = Input.GetKey(keyRun);

        jump = Input.GetKeyDown(keyJump);
        attack = Input.GetKeyDown(keyAttack);
    }
}
