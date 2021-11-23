using FeoFun.Core;
using UnityEngine;

namespace FeoFun.Camera
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float heightOffset = 0.1f;
        
        private int _initTowerSize;
        private float _lerp;
        private Quaternion _targetRotation;
        private Quaternion _startedRotation;
        private Vector3 _initDirection;

        private void Start()
        {
            TowerController.ONChangedTowerSize += UpdateCameraRotation;
            GameManager.ONGameRestart += InitStartingRotation;
            
            _initTowerSize = TowerController.Instance.TowerSize;
            _initDirection = transform.forward;
            InitStartingRotation();
        }
        
        private void OnDestroy()
        {
            TowerController.ONChangedTowerSize -= UpdateCameraRotation;
            GameManager.ONGameRestart -= InitStartingRotation;
        }
        
        private void InitStartingRotation()
        {
            UpdateCameraRotation(_initTowerSize);
            transform.rotation = _targetRotation;
            _lerp = 1;
        }

        private void LateUpdate()
        {
            if (_lerp < 1)
            {
                _lerp += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(_startedRotation, _targetRotation, _lerp);
            }
        }

        private void UpdateCameraRotation(int towerSize)
        {
            if(towerSize == 0)
                return;

            Vector3 currentPosition = transform.position;
            Vector3 focusPoint = currentPosition + _initDirection + Vector3.up * (towerSize - 1) * heightOffset;
            _targetRotation = Quaternion.LookRotation(focusPoint - currentPosition);
            _startedRotation = transform.rotation;
            _lerp = 0;
        }
    }
}