using FeoFun.Core;
using UnityEngine;

namespace FeoFun
{
    public class NeutralCubesManager : MonoBehaviour
    {
        [SerializeField] private NeutralCube neutralCubePrefab;

        private Vector3[] _neutralCubeInitPositions;

        private void Start()
        {
            CacheStartedPositions();
            GameManager.ONGameRestart += ResetPositions;
        }

        private void OnDestroy()
        {
            GameManager.ONGameRestart -= ResetPositions;
        }

        private void CacheStartedPositions()
        {
            var neutralCubes = GetComponentsInChildren<NeutralCube>();
            _neutralCubeInitPositions = new Vector3[neutralCubes.Length];
            for (int i = 0; i < neutralCubes.Length; i++)
            {
                _neutralCubeInitPositions[i] = neutralCubes[i].transform.position;
            }
        }

        private void ResetPositions()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < _neutralCubeInitPositions.Length; i++)
            {
                var neutralCube = Instantiate(neutralCubePrefab, transform);
                neutralCube.transform.position = _neutralCubeInitPositions[i];
            }
        }
    }
}
