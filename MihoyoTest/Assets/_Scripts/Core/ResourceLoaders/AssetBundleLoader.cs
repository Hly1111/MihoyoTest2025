using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class AssetBundleLoader : IResourceLoader
    {
        private readonly string _basePath = Application.streamingAssetsPath + "/AssetBundles/";
        
        public T Load<T>(string path) where T : Object
        {
            string bundlePath = System.IO.Path.Combine(_basePath, path);
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            if (bundle == null)
            {
                Debug.LogWarning($"AssetBundleLoader: AssetBundle not found at {bundlePath}");
                return null;
            }
            T asset = bundle.LoadAsset<T>(path);
            if (asset is GameObject)
            {
                asset = GameObject.Instantiate(asset);
            }
            bundle.Unload(false);
            return asset;
        }

        public IEnumerator LoadAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
            //Load Bundle
            string bundlePath = System.IO.Path.Combine(_basePath, path);
            AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
            yield return bundleRequest;
            AssetBundle bundle = bundleRequest.assetBundle;
            if (bundle == null)
            {
                Debug.LogWarning($"AssetBundleLoader: AssetBundle not found at {bundlePath}");
                callback?.Invoke(null);
                yield break;
            }
            //Load Asset
            AssetBundleRequest assetRequest = bundle.LoadAssetAsync<T>(path);
            yield return assetRequest;
            T asset = assetRequest.asset as T;
            if (asset is GameObject)
            {
                asset = GameObject.Instantiate(asset);
            }
            callback?.Invoke(asset);
            bundle.Unload(false);
        }
    }
}

