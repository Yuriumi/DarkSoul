using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConrol : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform playerHandle;
    private Transform cameraHandle;
    private GameObject playerModel;
    new private GameObject camera;

    public float horizontalSpeed;
    public float verticalSpeed;

    private float tempEulerX = 0;
    private Vector3 EulerX => new Vector3(tempEulerX, 0,0);

    private Vector3 cameraDampVelocity;

    private void Awake()
    {
        camera = Camera.main.gameObject;
        cameraHandle = transform.parent.transform;
        playerHandle = cameraHandle.parent.transform;
        playerInput = playerHandle.GetComponent<PlayerInput>();
        playerModel = playerHandle.GetComponent<ActorControl>().playerModel;
    }

    private void FixedUpdate()
    {
        Vector3 tempModeEuler = playerModel.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, playerInput.Jright * horizontalSpeed * Time.fixedDeltaTime);
        //cameraHandle.Rotate(Vector3.right, playerInput.Jup * verticalSpeed * Time.deltaTime);
        tempEulerX -= playerInput.Jup * -verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.localEulerAngles = EulerX;

        playerModel.transform.eulerAngles = tempModeEuler;

        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position,ref cameraDampVelocity,0.1f);
        //camera.transform.position = transform.position;
        //camera.transform.eulerAngles = transform.eulerAngles;
        camera.transform.LookAt(cameraHandle.transform);
    }
}
