using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    /// <summary>
    /// DESIGN PATTERN: Object Pool
    /// </summary>
    public readonly struct ObjectType
    {
        private readonly GameObject _fatherObj;
        public readonly List<GameObject> ObjList;
        public ObjectType(GameObject obj, GameObject poolObj)
        {
            _fatherObj= new GameObject(obj.name+"_Pool");
            _fatherObj.transform.parent = poolObj.transform;
            ObjList = new List<GameObject>() { obj };
            obj.transform.parent = _fatherObj.transform;
            if (obj.TryGetComponent(out IPoolObject poolObject))
            {
                poolObject.OnDeactivate();
            }
            obj.SetActive(false);
        }
        public void ReturnObject(GameObject obj)
        {
            ObjList.Add(obj);
            obj.transform.parent = _fatherObj.transform;
            if (obj.TryGetComponent(out IPoolObject poolObject))
            {
                poolObject.OnDeactivate();
            }
            obj.SetActive(false);
        }
        public GameObject GetObject()
        {
            GameObject obj;
            obj = ObjList[0];
            ObjList.RemoveAt(0);
            obj.SetActive(true);
            obj.transform.parent = null;
            if (obj.TryGetComponent(out IPoolObject poolObject))
            {
                poolObject.OnActivate();
            }
            return obj;
        }
    }
    
    /// <summary>
    /// DESIGN PATTERN: Object Pool, Singleton, Registration Factory
    /// </summary>
    public class ObjectPool : Singleton<ObjectPool>
    {
        private readonly Dictionary<string, ObjectType> _poolDic = new Dictionary<string, ObjectType>();
        private GameObject _poolObj;
        private readonly string _basePath = "Prefabs/";

        public void GetObject(string name,UnityAction<GameObject> callback)
        {
            if (_poolDic.ContainsKey(name) && _poolDic[name].ObjList.Count > 0)
            {
                callback.Invoke(_poolDic[name].GetObject());
            }
            else
            {
                ResourceManager.Instance.ResourceLoadAsync<GameObject>(_basePath + name, (o) =>
                {
                    o.name = name; 
                    callback(o); 
                });
            }
        }
        
        public void ReturnObject(string name, GameObject obj)
        {
            if (!_poolObj)
            {
                _poolObj = new GameObject("ObjectPool");
            }
            if (_poolDic.TryGetValue(name, out ObjectType objectType))
            {
                objectType.ReturnObject(obj);
            }
            else
            {
                _poolDic.Add(name, new ObjectType(obj, _poolObj));
            }
        }
        
        public void Clear()
        {
            _poolDic.Clear();
            _poolObj = null;
        }
        
    }
}

