using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    private bool _isEntered;
    
    private void OnCollisionEnter(Collision other)
    {
        if(_isEntered)
            return;
        
        TowerCube towerCube = other.gameObject.GetComponent<TowerCube>();
        if (towerCube && Vector3.Dot(Vector3.forward, other.impulse.normalized) != 0)
        {
            Debug.Log("Collision with enemy cube");
            TowerController.Instance.RemoveFromTower(towerCube);
            _isEntered = true;
        }
    }
}
