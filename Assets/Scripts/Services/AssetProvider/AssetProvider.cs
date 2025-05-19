using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services.AssetProvider
{
    public class AssetProvider: IAssetProvider
    {
        public async Task<T> LoadPersistence<T>(string address) where T : class
        {
            return await Addressables.LoadAssetAsync<T>(address).Task;
        }
    }
}