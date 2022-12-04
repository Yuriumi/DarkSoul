using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capcol;

    private Vector3 point1;
    private Vector3 point2;
    public float radius = 0.1f;

    private Collider[] outPutCols = new Collider[1];
    public LayerMask groundMask;
    private bool IsOnGrounded => Physics.OverlapSphereNonAlloc(transform.position, radius, outPutCols, groundMask) != 0;

    private void Awake()
    {

    }

    private void Update()
    {
        if (IsOnGrounded)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
