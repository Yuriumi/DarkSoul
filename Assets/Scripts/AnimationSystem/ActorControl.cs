using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorControl : MonoBehaviour
{
    public GameObject playerModel;
    public PlayerInput playerInput;
    public float walkSpeed = 2f;
    public float runSpeedScale = 2f;

    private Animator animator;
    new private Rigidbody rigidbody;
    private Vector3 movingVec;
    private Vector3 moveVelocity;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = playerModel.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animator.SetFloat("forward", playerInput.Dmag * (playerInput.run ? runSpeedScale : 1.0f));

        if (playerInput.Dmag > 0.1f)
        {
            playerModel.transform.forward = playerInput.Dvec;
        }

        movingVec = playerInput.Dmag * playerModel.transform.forward;
    }

    private void FixedUpdate()
    {
        // rigidbody.position += movingVec * Time.fixedDeltaTime * walkSpeed;
        moveVelocity.Set(movingVec.x, rigidbody.velocity.y, movingVec.z);
        rigidbody.velocity = moveVelocity * walkSpeed * (playerInput.run? runSpeedScale : 1.0f);
    }
}
