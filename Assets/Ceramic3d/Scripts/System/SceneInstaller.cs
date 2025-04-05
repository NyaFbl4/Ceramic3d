using UnityEngine;
using Zenject;

namespace Ceramic3d
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private CubesContainer _cubesContainer;

        private MatrixSolver _matrixSolver;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MatrixDataLoader>()
                .AsSingle();

            Container
                .Bind<MatrixJsonExample>()
                .AsSingle();

            Container
                .Bind<CubesContainer>()
                .FromInstance(_cubesContainer)
                .AsSingle();

            Container
                .Bind<MatrixSolver>()
                .AsSingle();
        }
    }
}