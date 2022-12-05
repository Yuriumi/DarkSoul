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
    [SerializeField] private float rollForce = 2;

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
        if (rigidbody.velocity.magnitude > 5.0f)
        {
            animator.SetTrigger("roll");
        }
        if (playerInput.attack)
        {
            if (CheckState("Ground"))
            {
                animator.SetTrigger("attack");
            }
        }
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

    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex(layerName)).IsName(stateName);
    }

    #region Message block
    public void OnJumpEnter()
    {
        playerInput.inputEnable = false;
        lockPlanar = true;
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    //public void OnJumpExit()
    //{
    //    playerInput.inputEnable = true;
    //    lockPlanar = false;
    //}

    public void IsGround()
    {
        animator.SetBool("isGround", true);
    }
    
    public void IsNotGround()
    {
        animator.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        playerInput.inputEnable = true;
        lockPlanar = false;
    }

    public void OnAttack1hAUpdate()
    {
        
    }
    public void OnFallEnter()
    {
        playerInput.inputEnable = true;
        lockPlanar = false;
    }

    public void OnRollEnter()
    {
        rigidbody.AddForce((playerModel.transform.forward + playerModel.transform.up).normalized * rollForce, ForceMode.Impulse);
    }

    public void OnJabEnter()
    {
        rigidbody.AddForce((-playerModel.transform.forward + playerModel.transform.up).normalized * rollForce, ForceMode.Impulse);
    }

    public void OnAttack1hAEnter()
    {
        playerInput.inputEnable = false;
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 1.0f);
        rigidbody.AddForce(playerModel.transform.forward, ForceMode.Impulse);
    }

    public void OnAttackIdle()
    {
        playerInput.inputEnable = true;
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 0f);
    }
    #endregion
}
