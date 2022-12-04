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
    private Vector3 planarVec;
    private Vector3 moveVelocity;
    private float targetRunScale;
    [SerializeField] private float jumpForce = 5;

    private bool lockPlanar = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = playerModel.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        targetRunScale = playerInput.run ? runSpeedScale : 1.0f;
        animator.SetFloat("forward", playerInput.Dmag * Mathf.Lerp(animator.GetFloat("forward"),targetRunScale,0.1f));
        if (playerInput.jump)
        {
            animator.SetTrigger("jump");
        }

        if (playerInput.Dmag > 0.1f)
        {
            playerModel.transform.forward = Vector3.Slerp(playerModel.transform.forward, playerInput.Dvec, 0.1f);
        }

        if (lockPlanar == false)
        {
            planarVec = playerInput.Dmag * playerModel.transform.forward;
        }

        //planarVec = playerInput.Dmag * playerModel.transform.forward;
    }

    private void FixedUpdate()
    {
        //moveVelocity.Set(movingVec.x, rigidbody.velocity.y, movingVec.z);
        //rigidbody.velocity = moveVelocity * walkSpeed * (playerInput.run ? runSpeedScale : 1.0f);
        rigidbody.MovePosition(rigidbody.position += planarVec * Time.fixedDeltaTime * walkSpeed * (playerInput.run ? runSpeedScale : 1.0f));
    }

    public void OnJumpEnter()
    {
        playerInput.inputEnable = false;
        lockPlanar = true;
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    public void OnJumpExit()
    {
        playerInput.inputEnable = true;
        lockPlanar = false;
    }
}
