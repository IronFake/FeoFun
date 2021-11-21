using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerController : Singleton<TowerController>
{
    public static event Action<int> ONChangedTowerSize;
    
    [SerializeField] private Transform neutralCubesParent;
    [SerializeField] private NeutralCube neutralCubePrefab;
    [SerializeField] private TowerCube towerCubePrefab;

    private readonly List<TowerCube> _towerParts = new List<TowerCube>();
    private int _startedTowerSize;
    private Vector3 _startedPos;
    
    public List<TowerCube> TowerParts => _towerParts;
    public TowerCube FirstCube => _towerParts[0];
    public int TowerSize => _towerParts.Count;
    
    private void Start()
    {
        foreach (Transform child in transform)
        {
            TowerCube cube = child.gameObject.GetComponent<TowerCube>();
            if (cube)
            {
                _towerParts.Add(cube);
            }
        }
        
        _towerParts[0].FirstCube = true;
        _startedTowerSize = TowerSize;
        _startedPos = transform.position;
    }

    public void AddToTower(NeutralCube neutralCube)
    {
        var cube = Instantiate(towerCubePrefab, transform);
        cube.transform.localPosition = Vector3.up * (transform.childCount - 1);
        _towerParts.Add(cube);
        Destroy(neutralCube.gameObject);
        
        ONChangedTowerSize?.Invoke(_towerParts.Count);
    }

    public void RemoveFromTower(TowerCube cube)
    {
        cube.IsNeutral = true;
        cube.FirstCube = false;
        cube.transform.parent = neutralCubesParent;
        _towerParts.Remove(cube);
        if (_towerParts.Count > 0)
        {
            _towerParts[0].FirstCube = true;
        }
        else
        {
            //TODO: end 
            GameManager.Instance.FinishGame();
        }
        
        ONChangedTowerSize?.Invoke(_towerParts.Count);
    }

    public void PrepareToStart()
    {
        if (_towerParts.Count > _startedTowerSize)
        {
            for (int i = _towerParts.Count - 1; i > _startedTowerSize; i--)
            {
                Destroy(_towerParts[i].gameObject);
                _towerParts.RemoveAt(i);
            }
        }
        else
        {
            for (int i = _towerParts.Count; i < _startedTowerSize; i++)
            {
                var cube = Instantiate(towerCubePrefab, transform);
                cube.transform.localPosition = Vector3.up * (transform.childCount - 1);
                _towerParts.Add(cube);
            }
        }

        transform.position = _startedPos;
    }
}
