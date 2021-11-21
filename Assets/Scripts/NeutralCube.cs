using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralCube : MonoBehaviour
{
    private bool _isEntered;
    
    private void OnTriggerEnter(Collider other)
    {
        if(_isEntered)
            return;
        
        TowerCube towerCube = other.gameObject.GetComponent<TowerCube>();
        if (towerCube)
        {
            Debug.Log("Collision with tower cube");
            TowerController.Instance.AddToTower(this);
            _isEntered = true;
        }
    }
}
