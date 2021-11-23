using UnityEngine;

namespace FeoFun.Core
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float height;

        public bool IsGrounded { get; private set; }

        private void FixedUpdate()
        {
            IsGrounded = Physics.Raycast(transform.position, Vector3.down, height, groundMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * height);
        }
    }
}
