using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    /// <summary>
/// DESIGN PATTERN: Template Method, Observer, Registration Factory
/// </summary>
public abstract class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> _controlDict = new Dictionary<string, List<UIBehaviour>>();
    public virtual void OnShow(){}
    public virtual void OnHide(){}
    
    protected virtual void Awake()
    {
        FindChildController<Button>();
        FindChildController<TMP_Text>();
        FindChildController<Image>();
        FindChildController<Toggle>();
        FindChildController<Slider>();
        FindChildController<ScrollRect>();
        FindChildController<TMP_InputField>();
        FindChildController<TMP_Dropdown>();
    }
    
    protected T GetController<T>(string controllerName) where T: UIBehaviour
    {
        if (_controlDict.ContainsKey(controllerName))
        {
            for (int i = 0; i < _controlDict[controllerName].Count; i++)
            {
                if (_controlDict[controllerName][i] is T)
                {
                    return _controlDict[controllerName][i] as T;
                }
            }
        }
        return null;
    }

    private void FindChildController<T>() where T : UIBehaviour
    {
        T[] ts = this.GetComponentsInChildren<T>();

        foreach (var item in ts)
        {
            string objName = item.gameObject.name;
            if (_controlDict.ContainsKey(objName))
            {
                _controlDict[objName].Add(item);
            }
            else
            {
                _controlDict.Add(objName, new List<UIBehaviour>() { item });
            }

            if (item is Button button)
            {
                button.onClick.AddListener(() => { OnClick(objName); });
            }
            else if (item is Toggle toggle)
            {
                toggle.onValueChanged.AddListener((value) => { OnValueChanged(objName, value); });
            }
            else if (item is Slider slider)
            {
                slider.onValueChanged.AddListener((value) => { OnValueChanged(objName, value); });
            }
            else if (item.gameObject.GetComponent<TMP_InputField>() != null)
            {
                item.gameObject.GetComponent<TMP_InputField>().onValueChanged.AddListener(
                    (value) => { OnValueChanged(objName, value); });
            }
            else if (item is ScrollRect rect)
            {
                rect.onValueChanged.AddListener((value) => { OnValueChanged(objName, value); });
            }
            else if (item.gameObject.GetComponent<TMP_Dropdown>() != null)
            {
                item.gameObject.GetComponent<TMP_Dropdown>().onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
        }
    }

    protected T AddNewController<T>(string objectName, UnityAction<T> callback = null) where T : UIBehaviour
    {
        T item;
        if(_controlDict.ContainsKey(objectName))
        {
            if(_controlDict[objectName].Count > 0)
            {
                foreach(var controller in _controlDict[objectName])
                {
                    if(controller is T)
                    {
                        Debug.LogWarning("Controller already exists");
                        return controller as T;
                    }
                }
            }
            item = _controlDict[objectName][0].gameObject.AddComponent<T>();
            _controlDict[objectName].Add(item);
        }
        else
        {
            item = new GameObject().AddComponent<T>();
            item.transform.SetParent(transform);
            item.gameObject.name = objectName;
            _controlDict.Add(objectName, new List<UIBehaviour>() { item });
        }
        callback?.Invoke(item);
        return item;
    }

    protected virtual void OnClick(string butName)
    {
        
    }
    protected virtual void OnValueChanged(string toggleName,bool value)
    {
        
    }
    protected virtual void OnValueChanged(string sliderName, float value)
    {

    }
    protected virtual void OnValueChanged(string inputFieldName, string value)
    {

    }
    protected virtual void OnValueChanged(string scrollRectName, Vector2 value)
    {

    }
    protected virtual void OnValueChanged(string dropDownName, int value)
    {

    }
}
}

