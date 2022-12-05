using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConrol : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform playerHandle;
    private Transform cameraHandle;

    public float horizontalSpeed;
    public float verticalSpeed;

    private float tempEulerX = 0;
    private Vector3 EulerX => new Vector3(tempEulerX, 0,0);

    private void Awake()
    {
        cameraHandle = transform.parent.transform;
        playerHandle = cameraHandle.parent.transform;
        playerInput = playerHandle.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        playerHandle.transform.Rotate(Vector3.up, playerInput.Jright * horizontalSpeed * Time.deltaTime);
        //cameraHandle.Rotate(Vector3.right, playerInput.Jup * verticalSpeed * Time.deltaTime);
        tempEulerX -= playerInput.Jup * -verticalSpeed * Time.deltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.localEulerAngles = EulerX;
    }
}
