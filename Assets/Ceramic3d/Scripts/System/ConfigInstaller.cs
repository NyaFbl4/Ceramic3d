using UnityEngine;
using Zenject;

namespace Ceramic3d
{
    [CreateAssetMenu(fileName = "ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private JsonConfig _jsonConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<JsonConfig>()
                .FromInstance(_jsonConfig)
                .AsSingle();
        }
    }
}