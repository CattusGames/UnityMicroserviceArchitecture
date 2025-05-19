using Infrastructure.StateMachine;
using Infrastructure.StateMachine.Game;
using Infrastructure.StateMachine.Game.States;
using Services.AssetProvider;
using Services.Factories.GameFactory;
using Services.InputHandler;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        public override void InstallBindings()
        {
            Debug.Log("InstallBindings");
            
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this);
            
            BindMonoServices();
            BindSceneLoader();
            BindServices();
            BindGameStateMachine();
            BindGameStates();
        }

        private void BindMonoServices()
        {
            Container.Bind<ICoroutineRunner>().FromMethod(() => Container.InstantiatePrefabForComponent<ICoroutineRunner>(_coroutineRunner)).AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<GameStates.Bootstrap>().AsSingle();
            Container.Bind<GameStates.LoadGame>().AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindInterfacesTo<GameStateMachine>().AsSingle();
        }

        private void BindSceneLoader()
        { 
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
        }
        
        private void BindServices()
        {
            BindAssetProvider();
            
            Container.BindInterfacesAndSelfTo<TouchInput>().AsSingle(); 
            
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().FromMethod(() => Container.Instantiate<AssetProvider>()).AsSingle();
        }

        public void Initialize()
        {
            Debug.Log("Initializing bootstrap installer");

            BootstrapGame();
        }

        private void BootstrapGame() => 
            Container.Resolve<IStateMachine<IGameState>>().Enter<GameStates.Bootstrap>();
    }
}
