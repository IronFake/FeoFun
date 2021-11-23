using UnityEngine;

namespace FeoFun.Camera
{
    [ExecuteInEditMode]
    public class FollowerCamera : MonoBehaviour
    {
        [SerializeField] private Transform followingFor;
        [SerializeField] private bool isFollow;

        private void LateUpdate()
        {
#if UNITY_EDITOR
            if (followingFor && isFollow)
                transform.position = followingFor.position;
#else
        transform.position = followingFor.position;
#endif
        }
    }
}
