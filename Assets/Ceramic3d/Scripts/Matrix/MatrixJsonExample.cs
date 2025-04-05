using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Ceramic3d
{
    public class MatrixJsonExample
    {
        private string _folderPath = Application.dataPath + "/Ceramic3d/Resources/JSON/";

        public void SaveMatrixToJson(List<Matrix4x4> matrix)
        {
            var wrapper = new MatrixJsonWrapper()
            {
                matrices = new List<MatrixJson>()
            };

            foreach (var m in matrix)
            {
                wrapper.matrices.Add(MatrixJson.FromMatrix4x4(m));
            }

            string json = JsonUtility.ToJson(wrapper, true);

            File.WriteAllText(_folderPath + "/Offset.json", json);
        }
    }
}