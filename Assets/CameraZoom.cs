using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private int[] towerCountStep;

    private Vector3 _startedCameraPos;
    private int _towerSize;
    private Vector3 _targetPos;
    
    private void Start()
    {
        TowerController.ONChangedTowerSize += UpdateCameraZoom;
        _startedCameraPos = transform.localPosition;
        _towerSize = TowerController.Instance.TowerSize;
    }
    
    private void OnDestroy()
    {
        TowerController.ONChangedTowerSize -= UpdateCameraZoom;
    }

    private void Update()
    {
        _targetPos = _startedCameraPos + -transform.forward * _towerSize;
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, Time.deltaTime);
    }

    private void UpdateCameraZoom(int towerSize)
    {
        _towerSize = towerSize;
    }
}
