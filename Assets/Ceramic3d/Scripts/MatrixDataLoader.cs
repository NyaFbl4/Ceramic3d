using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ceramic3d
{
    public class MatrixDataLoader : MonoBehaviour
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

        [Header("JSON Files")]
        public TextAsset modelJson;
        public TextAsset spaceJson;

        [Space]
        [Header("Loaded Data")]
        [SerializeField] private List<Matrix4x4> _modelMatrices = new List<Matrix4x4>();
        [SerializeField] private List<Matrix4x4> _spaceMatrices = new List<Matrix4x4>();

        private void Start()
        {
            //LoadAllMatrices();
        }
        
        public void LoadAllMatrices()
        {
            _modelMatrices = LoadMatricesFromJson(modelJson);
            _spaceMatrices = LoadMatricesFromJson(spaceJson);

            Debug.Log($"Loaded {_modelMatrices?.Count ?? 0} model matrices");
            Debug.Log($"Loaded {_spaceMatrices?.Count ?? 0} space matrices");
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
                Debug.LogError("JSON file reference is null!");
                return new List<Matrix4x4>();
            }

            try
            {
                // Добавляем обертку для корректного парсинга
                string wrappedJson = $"{{\"matrices\":{jsonFile.text}}}";
                MatrixJsonWrapper wrapper = JsonUtility.FromJson<MatrixJsonWrapper>(wrappedJson);

                if (wrapper?.matrices == null)
                {
                    Debug.LogError("Failed to parse JSON - invalid format");
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
                Debug.LogError($"Error loading matrices: {e.Message}");
                return new List<Matrix4x4>();
            }
        }
    }
}