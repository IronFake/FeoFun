using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerJumper : MonoBehaviour
{
    [SerializeField] private float jumpShortSpeed = 3f;
    [SerializeField] private float jumpSpeed = 1;
    //[SerializeField] private GroundCheck groundCheck;
    
    private bool _jump = false;
    private bool _jumpCancel = false;
    private TowerController _towerController;
    
    private void Start()
    {
        _towerController = TowerController.Instance;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Jump") && _towerController.FirstCube.IsGrounded)
        {
            _jump = true;
        }

        if (Input.GetButtonDown("Jump") && !_towerController.FirstCube.IsGrounded)
        {
            _jumpCancel = true; 
        }
    }

    private void FixedUpdate()
    {
        if (_jump)
        {
            ApplyVelocity(Vector3.up * jumpSpeed);
            _jump = false;
        }

        if (_jumpCancel)
        {
            ClampVelocity();
            _jumpCancel = false;
        }
    }

    private void ClampVelocity()
    {
        foreach (var towerPart in _towerController.TowerParts)
        {
            towerPart.ClampVelocity(jumpShortSpeed);
        }
    }

    private void ApplyVelocity(Vector3 newVelocity)
    {
        foreach (var towerPart in _towerController.TowerParts)
        {
            towerPart.SetVelocity(newVelocity);
        }
    }
}
