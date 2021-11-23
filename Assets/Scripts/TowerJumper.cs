using System.Collections;
using FeoFun.Inputs;
using UnityEngine;

namespace FeoFun.Core
{
    public class TowerJumper : MonoBehaviour
    {
        [SerializeField] private float shortJumpSpeed = 3f;
        [SerializeField] private float jumpSpeed = 1;
        [SerializeField] private float velocityStep;
        [SerializeField] private float delayForAddVelocity;

        private InputManager _inputManager;
        private TowerController _towerController;
        private bool _jump;
        private bool _jumpCancel;
        private WaitForSeconds _waitForAddVelocity;

        private void Start()
        {
            _towerController = TowerController.Instance;
            _inputManager = InputManager.Instance;

            _waitForAddVelocity = new WaitForSeconds(delayForAddVelocity);
        }

        private void Update()
        {
            if (!_jump && _inputManager.TouchBegan && _towerController.FirstCube.IsGrounded)
            {
                _jump = true;
                Debug.Log("Jump");
            }

            if (_inputManager.TouchEnded && !_towerController.FirstCube.IsGrounded)
            {
                _jumpCancel = true;
                Debug.Log("JumpCanceled");
            }
        }

        private void FixedUpdate()
        {
            if (_jump)
            {
                StartCoroutine(ApplyVelocityWithDelay(Vector2.up * jumpSpeed));
                _jump = false;
            }

            if (_jumpCancel)
            {
                ClampVelocity(shortJumpSpeed);
                _jumpCancel = false;
            }
        }

        private void ClampVelocity(float value)
        {
            var towerParts = _towerController.TowerParts;
            for (int i = towerParts.Count - 1; i >= 0; i--)
            {
                towerParts[i].ClampVelocity(value);
            }
        }

        IEnumerator ApplyVelocityWithDelay(Vector3 newVelocity)
        {
            var towerParts = _towerController.TowerParts;
            for (int i = towerParts.Count - 1; i >= 0; i--)
            {
                newVelocity.y += velocityStep * i;
                towerParts[i].SetVelocity(newVelocity);
                yield return _waitForAddVelocity;
            }
        }
    }
}
