using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ceramic3d
{
    public class CubesContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private List<GameObject> _cubes;
        
        private MatrixDataLoader _matrixDataLoader;

        [Inject]
        public void Construct(MatrixDataLoader matrixDataLoader)
        {
            _matrixDataLoader = matrixDataLoader;
        }
        
        [Button]
        public void SpawnCubes()
        {
            var spaceMatrices = _matrixDataLoader.GetSpaceMatrix();
            
            foreach (var matrix in spaceMatrices)
            {
                GameObject cube = Instantiate(_cubePrefab);
                cube.transform.position = matrix.GetPosition();
                
                _cubes.Add(cube);
            }
        }

        public void PaintCube(int number)
        {
            GameObject cube = _cubes[number];
            
            cube.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}