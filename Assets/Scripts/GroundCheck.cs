using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float height;
    
    public bool IsGrounded { get; private set; }

    // private void OnCollisionStay(Collision other)
    // {
    //     IsGrounded = other != null && other.gameObject.layer == groundMask.value;
    // }
    //
    // private void OnCollisionExit(Collision other)
    // {
    //     IsGrounded = false;
    // }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        Color rayColor;
        if (Physics.Raycast(ray, out hit, height, groundMask))
        {
            IsGrounded = true;
            rayColor = Color.green;
        }
        else
        {
            IsGrounded = false;
            rayColor = Color.red;
        }
        
        Debug.DrawRay(ray.origin, ray.direction * height, rayColor);
    }
}
