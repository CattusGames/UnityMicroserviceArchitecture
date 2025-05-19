

namespace Infrastructure.StateMachine.Game.States
{
    public abstract partial class GameStates
    {
        public class Bootstrap : IState, IGameState
        {
            private const string FIRST_SCENE_NAME = "Level_1";
            
            private readonly IStateMachine<IGameState> _stateMachine;
            private readonly ISceneLoader _sceneLoader;


            public Bootstrap(IStateMachine<IGameState> stateMachine, ISceneLoader sceneLoader)
            {
                _stateMachine = stateMachine;
                _sceneLoader = sceneLoader;
            }

            public void Enter()
            {
               _sceneLoader.Load(FIRST_SCENE_NAME, OnLevelLoad);
            }

            private void OnLevelLoad() => _stateMachine.Enter<LoadGame>();

            public void Exit()
            {
                
            }
        }
    }
}