using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core
{
    /// <summary>
    /// DESIGN PATTERN: Singleton, Facade
    /// </summary>
    public class SceneLoader : Singleton<SceneLoader>
    {
        public void LoadScene(string sceneName,UnityAction func)
        {
            SceneManager.LoadScene(sceneName);
            func.Invoke();
        }
        
        public void LoadSceneAsync(string sceneName,UnityAction<float> processCallback = null, UnityAction doneCallback = null)
        {
            CommonMono.Instance.StartCoroutine(ReallyLoadSceneAsync(sceneName,processCallback, doneCallback));
        }
        
        private IEnumerator ReallyLoadSceneAsync(string sceneName, UnityAction<float> progressCallback, UnityAction doneCallback)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
            while(ao != null && !ao.isDone)
            {
                float progress = Mathf.Clamp01(ao.progress / 0.9f);
                progressCallback?.Invoke(progress);
                if (ao.isDone)
                    break;
                yield return null;
            }
            doneCallback?.Invoke();
        }
    }
}
