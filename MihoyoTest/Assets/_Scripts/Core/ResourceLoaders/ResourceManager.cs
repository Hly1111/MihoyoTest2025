using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    
    public interface IResourceLoader
    {
        T Load<T>(string path) where T : Object;
        
        IEnumerator LoadAsync<T>(string path, UnityAction<T> callback) where T : Object;
    }
    
    /// <summary>
    /// DESIGN PATTERN: Singleton, Factory Method, Facade, Template Method, Chain of Responsibility
    /// </summary>
    public class ResourceManager : Singleton<ResourceManager>
    {
        private readonly List<IResourceLoader> _resourceLoaders = new List<IResourceLoader>();

        public ResourceManager()
        {
            _resourceLoaders.Add(new ResourceLoader());
            _resourceLoaders.Add(new AssetBundleLoader());
        }

        public T ResourceLoad<T>(string path) where T : Object
        {
            foreach (var loader in _resourceLoaders)
            {
                T resource = loader.Load<T>(path);
                if (resource != null)
                {
                    return resource;
                }
            }

            Debug.LogWarning($"Resource [{path}] of type {typeof(T)} not found.");
            return null;
        }

        public void ResourceLoadAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
            CommonMono.Instance.StartCoroutine(LoadAsyncChain(path, callback));
        }
        
        private IEnumerator LoadAsyncChain<T>(string path, UnityAction<T> callback) where T : Object
        {
            foreach (var loader in _resourceLoaders)
            {
                bool loaded = false;
                yield return CommonMono.Instance.StartCoroutine(loader.LoadAsync<T>(path, (res) => {
                    if (res != null)
                    {
                        callback?.Invoke(res);
                        loaded = true;
                    }
                }));
                if (loaded)
                    yield break;
            }
            Debug.LogWarning($"ResourceManager: Resource [{path}] of type {typeof(T)} not found in any loader.");
            callback?.Invoke(null);
        }
    }
}

