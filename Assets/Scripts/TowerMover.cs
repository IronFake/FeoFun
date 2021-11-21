using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMover : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    
    private void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }
}
