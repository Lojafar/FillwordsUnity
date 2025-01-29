using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FillWords.Root.AssetManagment
{
    class ResourcesAssetProvider : IAssetProvider
    {
        readonly Dictionary<string, object> loadedPrefabsMap;
        const string prefabsPathPattern = "Prefabs/{0}";
        const string assetsPathPattern = "Assets/{0}";
        const string configsPathPattern = "Configs/{0}";
        public ResourcesAssetProvider()
        {
            loadedPrefabsMap = new();
        }
        public async Task<T> LoadPrefab<T>(string Key) where T : Object
        {
            if(loadedPrefabsMap.TryGetValue(Key, out object asset))
            {
                return (T)asset;
            }

            await Task.Delay(100); // dummy
            T loadedAsset = (T)Resources.LoadAsync<T>(string.Format(prefabsPathPattern, Key)).asset;

            loadedPrefabsMap.Add(Key, loadedAsset);
            return loadedAsset;
        }
        public async Task<T> LoadAsset<T>(string Key) where T : Object
        {
            await Task.Delay(50);// dummy
            T loadedAsset = (T)Resources.LoadAsync<T>(string.Format(assetsPathPattern, Key)).asset;
            return loadedAsset;
        }
        public async Task<T> LoadConfig<T>(string Key) where T : ScriptableObject
        {
            await Task.Delay(50);// dummy
            T loadedAsset = (T)Resources.LoadAsync<T>(string.Format(configsPathPattern, Key)).asset;
            return loadedAsset;
        }
    }
}
