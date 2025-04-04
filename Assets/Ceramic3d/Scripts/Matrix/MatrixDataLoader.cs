using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ceramic3d
{
    public class MatrixDataLoader
    {
        [System.Serializable]
        private class MatrixJsonWrapper
        {
            public List<MatrixJson> matrices;
        }

        [System.Serializable]
        private class MatrixJson
        {
            public float m00, m01, m02, m03;
            public float m10, m11, m12, m13;
            public float m20, m21, m22, m23;
            public float m30, m31, m32, m33;

            public Matrix4x4 ToMatrix4x4()
            {
                return new Matrix4x4(
                    new Vector4(m00, m10, m20, m30),
                    new Vector4(m01, m11, m21, m31),
                    new Vector4(m02, m12, m22, m32),
                    new Vector4(m03, m13, m23, m33)
                );
            }
        }

        private readonly JsonConfig _jsonConfig;


        [Space]
        [Header("Loaded Data")]
        [SerializeField] private List<Matrix4x4> _modelMatrices = new List<Matrix4x4>();
        [SerializeField] private List<Matrix4x4> _spaceMatrices = new List<Matrix4x4>();
        
        public  MatrixDataLoader(JsonConfig config)
        {
            _jsonConfig = config;
        }
        
        public void LoadAllMatrixes()
        {
            _modelMatrices = LoadMatricesFromJson(_jsonConfig.ModelJson);
            _spaceMatrices = LoadMatricesFromJson(_jsonConfig.SpaceJson);

            Debug.Log($"Loaded {_modelMatrices?.Count ?? 0} model matrixes");
            Debug.Log($"Loaded {_spaceMatrices?.Count ?? 0} space matrixes");
        }
        
        public List<Matrix4x4> GetModelMatrix()
        {
            return _modelMatrices;
        }
        
        public List<Matrix4x4> GetSpaceMatrix()
        {
            return _spaceMatrices;
        }

        private List<Matrix4x4> LoadMatricesFromJson(TextAsset jsonFile)
        {
            if (jsonFile == null)
            {
                return new List<Matrix4x4>();
            }

            try
            {
                string wrappedJson = $"{{\"matrices\":{jsonFile.text}}}";
                MatrixJsonWrapper wrapper = JsonUtility.FromJson<MatrixJsonWrapper>(wrappedJson);

                if (wrapper?.matrices == null)
                {
                    return new List<Matrix4x4>();
                }

                List<Matrix4x4> result = new List<Matrix4x4>();
                foreach (MatrixJson matrixJson in wrapper.matrices)
                {
                    result.Add(matrixJson.ToMatrix4x4());
                }

                return result;
            }
            catch (Exception e)
            {
                return new List<Matrix4x4>();
            }
        }
    }
}