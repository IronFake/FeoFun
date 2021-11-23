using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FeoFun.Core
{
    public class TowerController : Singleton<TowerController>
    {
        public static event Action<int> ONChangedTowerSize;

        [SerializeField] private TowerMover towerMover;
        [SerializeField] private TowerJumper towerJumper;
        [SerializeField] private Transform neutralCubesParent;
        [SerializeField] private TowerCube towerCubePrefab;

        private readonly List<TowerCube> _towerParts = new List<TowerCube>();
        private int _startedTowerSize;
        private Vector3 _startedPos;

        public List<TowerCube> TowerParts => _towerParts;
        public TowerCube FirstCube => _towerParts[0];
        public int TowerSize => _towerParts.Count;
        public float CenterOfTower => (_towerParts.Count - 1) / 2f;

        protected override void Awake()
        {
            base.Awake();
            GameManager.ONGameStart += StartMove;
            GameManager.ONGameRestart += Reset;

            _startedTowerSize = transform.childCount;
            _startedPos = transform.position;
            Reset();
        }

        private void OnDestroy()
        {
            GameManager.ONGameStart -= StartMove;
            GameManager.ONGameRestart -= Reset;
        }

        private void StartMove()
        {
            towerMover.enabled = true;
            towerJumper.enabled = true;
        }

        private void Reset()
        {
            towerMover.enabled = false;
            towerJumper.enabled = false;
            PrepareToStart();
        }
        
        public void AddToTower(NeutralCube neutralCube)
        {
            var cube = Instantiate(towerCubePrefab, transform);
            cube.transform.localPosition = _towerParts.Last().transform.localPosition + Vector3.up;
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
                ONChangedTowerSize?.Invoke(_towerParts.Count);
            }
            else
            {
                GameManager.Instance.RestartGame();
            }
        }

        private void PrepareToStart()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            _towerParts.Clear();
            for (int i = 0; i < _startedTowerSize; i++)
            {
                var cube = Instantiate(towerCubePrefab, transform);
                cube.transform.localPosition = Vector3.up * i;
                _towerParts.Add(cube);
            }
            
            transform.position = _startedPos;
            _towerParts[0].FirstCube = true;
        }
    }
}
