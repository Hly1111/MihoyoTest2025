using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
// ReSharper disable All

namespace Core
{
    /// <summary>
    /// DESIGN PATTERN: Template Method, Facade, Composite
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public enum EUILayer
    {
        Bottom,
        Middle,
        Top,
        System,
    }
    public class UIManager : Singleton<UIManager>
    {
        private readonly Dictionary<string, BasePanel> _panelDict = new Dictionary<string, BasePanel>();
        private readonly Transform _bottom;
        private readonly Transform _middle;
        private readonly Transform _top;
        private readonly Transform _system;
        private readonly RectTransform canvas;
        private readonly RectTransform _inWorldCanvas;
        
        private readonly string _canvasPath = "Controls/Canvas";
        private readonly string _inWorldCanvasPath = "Controls/InWorldCanvas";
        private readonly string _eventSystemPath = "Controls/EventSystem";
        private readonly string _panelBasePath = "Controls/UI/";

        public UIManager()
        {
            GameObject obj = ResourceManager.Instance.ResourceLoad<GameObject>(_canvasPath);
            canvas = obj.transform as RectTransform;
            GameObject.DontDestroyOnLoad(obj);
            if (canvas != null)
            {
                _bottom = canvas.Find("Bottom");
                _middle = canvas.Find("Middle");
                _top = canvas.Find("Top");
                _system = canvas.Find("System");
            }

            obj = ResourceManager.Instance.ResourceLoad<GameObject>(_eventSystemPath);
            GameObject.DontDestroyOnLoad(obj);
            
            obj = ResourceManager.Instance.ResourceLoad<GameObject>(_inWorldCanvasPath);
            _inWorldCanvas = obj.transform as RectTransform;
        }

        public RectTransform GetCanvas()
        {
            return canvas;
        }
        
        public Transform GetFatherLayer(EUILayer layer)
        {
            switch (layer)
            {
                case EUILayer.Bottom:
                    return _bottom;
                case EUILayer.Middle:
                    return _middle;
                case EUILayer.Top:
                    return _top;
                case EUILayer.System:
                    return _system;
                default:
                    return null;
            }
        }
        
        public void ShowPanel<T>(string panelName, EUILayer layer = EUILayer.Middle, UnityAction<T> callback = null) where T : BasePanel
        {
            if(_panelDict.ContainsKey(panelName))
            {
                T panel = _panelDict[panelName] as T;
                panel?.OnShow();
                callback?.Invoke(_panelDict[panelName] as T);
                return;
            }
            ResourceManager.Instance.ResourceLoadAsync<GameObject>(_panelBasePath + panelName, (obj) =>
            {
                obj.name = panelName;
                Transform father = GetFatherLayer(layer);
                obj.transform.SetParent(father);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                ((obj.transform as RectTransform)!).offsetMax = Vector2.zero;
                ((obj.transform as RectTransform)!).offsetMin = Vector2.zero;
                T panel = obj.GetComponent<T>();
                panel.OnShow();
                _panelDict.Add(panelName, panel);
                callback?.Invoke(panel);
            });
        }
        
        public void ShowPanelInWorld<T>(string panelName, UnityAction<T> callback = null) where T : BasePanel
        {
            ResourceManager.Instance.ResourceLoadAsync<GameObject>(_panelBasePath + panelName, (obj) =>
            {
                obj.name = panelName;
                obj.transform.SetParent(_inWorldCanvas);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.transform.localRotation = Quaternion.identity;
                T panel = obj.GetComponent<T>();
                panel.OnShow();
                _panelDict.Add(panelName, panel);
                callback?.Invoke(panel);
            });
        }

        public void HidePanel(string panelName)
        {
            if(_panelDict.ContainsKey(panelName))
            {
                _panelDict[panelName].OnHide();
                CommonMono.Instance.DestroyObject(_panelDict[panelName].gameObject);
                _panelDict.Remove(panelName);
            }
        }

        public T GetPanel<T>(string panelName) where T : BasePanel
        {
            if(_panelDict.TryGetValue(panelName, out BasePanel value))
            {
                return value as T;
            }
            return null;
        }
        
        public void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> func)
        {
            EventTrigger trigger=control.GetComponent<EventTrigger>();
            if(trigger==null)
            {
                trigger = control.gameObject.AddComponent<EventTrigger>();
            }
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(func);
            trigger.triggers.Add(entry);
        }
    }
}

