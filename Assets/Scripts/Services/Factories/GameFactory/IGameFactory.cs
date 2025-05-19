using System.Threading.Tasks;
using UnityEngine;

namespace Services.Factories.GameFactory
{
    public interface IGameFactory
    {
        GameObject Player { get; }
        GameObject GameHud { get; }
        
        Task<GameObject> CreatePlayer(Camera playerCamera);
        Task<GameObject> CreateGameHud();
    }
}