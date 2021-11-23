using UnityEngine;

namespace FeoFun.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class TowerCube : MonoBehaviour
    {
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private Material towerMaterial;
        [SerializeField] private Material neutralMaterial;

        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private bool _isNeutral;
        private bool _firstCube;

        public bool IsGrounded => groundCheck.IsGrounded;

        public bool IsNeutral
        {
            get => _isNeutral;
            set
            {
                _isNeutral = value;
                _meshRenderer.material = _isNeutral ? neutralMaterial : towerMaterial;
            }
        }

        public bool FirstCube
        {
            get => _firstCube;
            set
            {
                _firstCube = value;
                groundCheck.enabled = _firstCube;
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();


        }
        
        public void SetVelocity(Vector3 newVelocity)
        {
            _rigidbody.velocity = newVelocity;
        }

        public void ClampVelocity(float maxVelocity)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
        }
    }
}
