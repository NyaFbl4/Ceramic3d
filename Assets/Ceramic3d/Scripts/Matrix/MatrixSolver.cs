using UnityEngine;
using System.Collections.Generic;

namespace Ceramic3d
{
    public class MatrixSolver
    {
        private readonly CubesContainer _cubesContainer;

        public MatrixSolver(CubesContainer cubesContainer)
        {
            _cubesContainer = cubesContainer;
        }
        
        public List<Matrix4x4> FindAllOffsets(List<Matrix4x4> modelMatrixes, 
            List<Matrix4x4> spaceMatrixes,
            float epsilon = 0.0001f)
        {
            List<Matrix4x4> offsets = new List<Matrix4x4>();

            int number = 0;
            
            foreach (Matrix4x4 m in modelMatrixes)
            {
                bool found = false;
                Matrix4x4 invM = m.inverse;

                foreach (Matrix4x4 s in spaceMatrixes)
                {
                    number++;
                    Matrix4x4 offset = s * invM;
                    
                    if (AreMatrixesEqual(offset * m, s, epsilon))
                    {
                        offsets.Add(offset);
                        found = true;
                        
                        _cubesContainer.PaintCube(number);
                        
                        break; 
                    }
                }
            }

            return offsets;
        }
        
        private bool AreMatrixesEqual(Matrix4x4 a, Matrix4x4 b, float epsilon)
        {
            for (int i = 0; i < 16; i++)
            {
                if (Mathf.Abs(a[i] - b[i]) > epsilon)
                {
                    return false;
                }
            }

            return true;
        }
    }
}