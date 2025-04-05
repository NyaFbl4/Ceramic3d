using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ceramic3d
{
    public class MatrixDataLoader
    {
        private readonly JsonConfig _jsonConfig;
        
        private List<Matrix4x4> _modelMatrices = new();
        private List<Matrix4x4> _spaceMatrices = new();
        
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