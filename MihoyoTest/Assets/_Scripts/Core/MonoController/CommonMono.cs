using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    /// <summary>
    /// DESIGN PATTERN: Lazy Initialization, Singleton, Facade, Observer
    /// </summary>
    public class CommonMono : Singleton<CommonMono>
    {
        private readonly CommonMonoController _controller;

        public CommonMono()
        {
            GameObject obj = new GameObject("CommonMonoController");
            _controller = obj.AddComponent<CommonMonoController>();
        }
        
        public void AddUpdate(UnityAction func)
        {
            _controller.AddUpdate(func);
        }
        
        public void RemoveUpdate(UnityAction func)
        {
            _controller.RemoveUpdate(func);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _controller.StartCoroutine(routine);
        }
        
        public void StopCoroutine(Coroutine routine)
        {
            _controller.StopCoroutine(routine);
        }
        
        public void DestroyObject(GameObject obj)
        {
            _controller.DestroyObject(obj);
        }
    }
}

