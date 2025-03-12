using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class ResourceLoader : IResourceLoader
    {
        public T Load<T>(string path) where T : Object
        {
            T resource = Resources.Load<T>(path);
            if (resource != null)
            {
                if (resource is GameObject)
                {
                    return GameObject.Instantiate(resource);
                }
                return resource;
            }
            return null;
        }

        public IEnumerator LoadAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
            ResourceRequest rr = Resources.LoadAsync(path);
            yield return rr;
            if (rr.asset != null)
            {
                if (rr.asset is GameObject)
                {
                    callback.Invoke(GameObject.Instantiate(rr.asset) as T);
                }
                else
                {
                    callback.Invoke(rr.asset as T);
                }
            }
            else
            {
                callback.Invoke(null);
            }
        }
    }
}

