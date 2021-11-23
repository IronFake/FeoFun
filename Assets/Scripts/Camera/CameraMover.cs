using FeoFun.Core;
using UnityEngine;

namespace FeoFun.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float distanceMlt = 1;

        private float _lerp;
        private int _initTowerSize;
        private Vector3 _targetPosition;
        private Vector3 _startedPosition;
        private Vector3 _initPosition;
        private Transform _cameraTransform;

        private void Start()
        {
            TowerController.ONChangedTowerSize += UpdateCameraPos;
            GameManager.ONGameRestart += InitStartingPos;

            _cameraTransform = transform;
            _initTowerSize = TowerController.Instance.TowerSize;
            _initPosition = _cameraTransform.localPosition;
            
            InitStartingPos();
        }

        private void OnDestroy()
        {
            TowerController.ONChangedTowerSize -= UpdateCameraPos;
            GameManager.ONGameRestart -= InitStartingPos;
        }

        private void InitStartingPos()
        {
            UpdateCameraPos(_initTowerSize);
            _cameraTransform.localPosition = _targetPosition;
            _lerp = 1;
        }

        private void LateUpdate()
        {
            if (_lerp < 1)
            {
                _lerp += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(_startedPosition, _targetPosition, _lerp);
            }
        }

        private void UpdateCameraPos(int towerSize)
        {
            if (towerSize == 0)
                return;

            _targetPosition = _initPosition + -_cameraTransform.forward * towerSize * distanceMlt;
            _startedPosition = _cameraTransform.localPosition;
            _lerp = 0;
        }
    }
}