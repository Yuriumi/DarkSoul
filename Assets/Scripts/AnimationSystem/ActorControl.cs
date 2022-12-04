using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorControl : MonoBehaviour
{
    public GameObject playerModel;
    public PlayerInput playerInput;

    private Animator animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = playerModel.GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("forward",playerInput.Dmag);
        playerModel.transform.forward = playerInput.Dvec;
    }
}
