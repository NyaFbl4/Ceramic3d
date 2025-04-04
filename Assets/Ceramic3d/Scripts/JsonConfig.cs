using UnityEngine;
using Zenject;

namespace Ceramic3d
{
    [System.Serializable]
    public class JsonConfig
    {
        [SerializeField] private TextAsset _modelJson;
        [SerializeField] private TextAsset _spaceJson;

        public TextAsset ModelJson => _modelJson;
        public TextAsset SpaceJson => _spaceJson;
    }
}