using System.Threading.Tasks;

namespace Services.AssetProvider
{
    public interface IAssetProvider
    {
        Task<T> LoadPersistence<T>(string address) where T : class;
    }
}