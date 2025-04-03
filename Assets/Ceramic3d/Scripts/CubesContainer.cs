using System.Collections.Generic;
using UnityEngine;

namespace Ceramic3d
{
    public class CubesContainer : MonoBehaviour
    {
        [SerializeField] private MatrixDataLoader _matrixDataLoader;

        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private List<GameObject> _cubes;

        private void Start()
        {

        }

        private void Update()
        {
            if (_cubes.Count == 0)
            {
                var spaceMatrices = _matrixDataLoader.GetSpaceMatrix();
                
                foreach (var matrix in spaceMatrices)
                {
                    GameObject cube = Instantiate(_cubePrefab);
                    cube.transform.position = matrix.GetPosition();
                
                    _cubes.Add(cube);
                }
            }
        }
    }
}