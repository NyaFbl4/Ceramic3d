using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ceramic3d
{
    public class Helper : MonoBehaviour
    {
        private MatrixDataLoader _matrixDataLoader;
        private CubesContainer _cubesContainer;
        private MatrixSolver _matrixSolver;

        [SerializeField] private List<Matrix4x4> _offset;
        
        [Inject]
        public void Container(MatrixDataLoader matrixDataLoader, CubesContainer cubesContainer,
            MatrixSolver matrixSolver)
        {
            _matrixDataLoader = matrixDataLoader;
            _cubesContainer = cubesContainer;
            _matrixSolver = matrixSolver;
        }

        [Button]
        private void SpawnCubes()
        {
            _matrixDataLoader.LoadAllMatrixes();
            _cubesContainer.SpawnCubes(); 
        }

        [Button]
        private void FindAllOffsets()
        {
            _offset = _matrixSolver.FindAllOffsets(
                _matrixDataLoader.GetModelMatrix(), _matrixDataLoader.GetSpaceMatrix());
        }
    }
}