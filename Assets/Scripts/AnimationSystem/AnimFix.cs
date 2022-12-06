using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFix : MonoBehaviour
{
    Animator animator;
    public HumanBodyBones leftArmBodyBones;

    public Vector3 fixLeftArm;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator.GetBool("defense") == false)
        {
            Transform leftArm = animator.GetBoneTransform(leftArmBodyBones);
            leftArm.localEulerAngles += fixLeftArm;
            animator.SetBoneLocalRotation(leftArmBodyBones, Quaternion.Euler(leftArm.localEulerAngles));
        }
    }
}
