using UnityEngine;

namespace FeoFun.Core
{
    public class Border : MonoBehaviour
    {
        [SerializeField] private float collisionLimitAngle = 45f;
        
        private bool _isEntered;
        
        private void Start()
        {
            GameManager.ONGameRestart += Reset;
        }

        private void OnDestroy()
        {
            GameManager.ONGameRestart -= Reset;
        }

        private void Reset()
        {
            _isEntered = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isEntered)
                return;

            float angle = Vector3.Angle(Vector3.back, other.transform.position - transform.position);
            
            TowerCube towerCube = other.gameObject.GetComponent<TowerCube>();
            if (towerCube && towerCube.FirstCube && angle < collisionLimitAngle)
            {
                _isEntered = true;
                Debug.Log("collision with enemy cube");
                TowerController.Instance.RemoveFromTower(towerCube);
            }
        }
    }
}
