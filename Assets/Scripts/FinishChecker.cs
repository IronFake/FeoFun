using UnityEngine;

namespace FeoFun.Core
{
    [RequireComponent(typeof(Collider))]
    public class FinishChecker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<TowerCube>())
            {
                GameManager.Instance.RestartGame();
            }
        }
    }
}
