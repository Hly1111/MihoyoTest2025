using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    /// <summary>
    /// DESIGN PATTERN: Front Controller
    /// </summary>
    public class CommonMonoController : MonoBehaviour
    {
        private event UnityAction updateEvent;
        
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
        private void Update()
        {
            updateEvent?.Invoke();
        }
        
        public void AddUpdate(UnityAction func)
        {
            updateEvent += func;
        }
        
        public void RemoveUpdate(UnityAction func)
        {
            updateEvent -= func;
        }
        
        public void DestroyObject(GameObject obj)
        {
            Destroy(obj);
        }
    }
}

