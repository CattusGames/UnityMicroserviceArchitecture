using System.Threading.Tasks;
using Game.Player;
using Services.Factories.GameFactory;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.StateMachine.Game.States
{
    public abstract partial class GameStates
    {
        public class LoadGame : IState, IGameState
        {
            private IGameFactory _gameFactory;

            public LoadGame(IGameFactory gameFactory)
            {
                _gameFactory = gameFactory;
            }
            public async void Enter()
            {
                Debug.Log("Load Game");
                
                await _gameFactory.CreateGameHud();
                
                await InitPlayer();
            }

            private async Task InitPlayer()
            {
                await _gameFactory.CreatePlayer(Camera.main);
                
                _gameFactory.Player.GetComponent<MovementController>().Initialize(_gameFactory.GameHud.GetComponentInChildren<Slider>());
            }

            public void Exit()
            {
            }
        }

    }
}