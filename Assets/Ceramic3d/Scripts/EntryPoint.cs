using System.Collections.Generic;
using UnityEngine;

namespace Ceramic3d
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MatrixDataLoader _matrixDataLoader;
        [SerializeField] private CubesContainer _cubesContainer;

        [SerializeField] private List<Matrix4x4> _offset;

        private void Start()
        {
            _matrixDataLoader.LoadAllMatrices();
            _cubesContainer.SpawnCubes();

            _offset = MatrixSolver.FindAllOffsets(_matrixDataLoader.GetModelMatrix(),
                _matrixDataLoader.GetSpaceMatrix());
        }
    }
}