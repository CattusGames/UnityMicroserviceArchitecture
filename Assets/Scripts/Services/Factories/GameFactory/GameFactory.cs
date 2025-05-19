using System.Threading.Tasks;
using Extensions;
using Services.AssetProvider;
using UnityEngine;
using Zenject;

namespace Services.Factories.GameFactory
{
    public class GameFactory: Factory, IGameFactory
    {
        
        private const string PlayerPrefabAddress = "Player";
        private const string GameHudAddress = "GameHud";
        
        private GameObject _playerPrefab;
        private GameObject _gameHudPrefab;
        
        private GameObject _uiRoot;
        
        private readonly IAssetProvider _assetProvider;
        
        public GameFactory(IInstantiator instantiator, IAssetProvider assetProvider) : base(instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public GameObject Player { get; private set; }
        public GameObject GameHud { get; private set; }
        

        public async Task<GameObject> CreatePlayer(Camera camera)
        {
            if (_playerPrefab == null)
                await LoadPlayer();
            
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane)); 
            position.z = 0f;
            
            Player = Instantiate(_playerPrefab, position, Quaternion.identity, null);
            
            FitSpriteRendererSize.SetObjectSizeInPixels(Player, 50f, camera);
            return Player;
        }
        
        public async Task<GameObject> CreateGameHud()
        {
            if (_gameHudPrefab == null)
                await LoadGameHud();
            
            return GameHud = Instantiate(_gameHudPrefab);
        }
        
        private async Task LoadGameHud() => 
            _gameHudPrefab = await _assetProvider.LoadPersistence<GameObject>(GameHudAddress);
        
        private async Task LoadPlayer() => 
            _playerPrefab = await _assetProvider.LoadPersistence<GameObject>(PlayerPrefabAddress);
    }
}