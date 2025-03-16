using System.Collections;
using UnityEngine;

/// <summary>
/// DESIGN PATTERN: Template Method
/// </summary>
public interface IPoolObject
{
    public void OnActivate();
    public void OnDeactivate();
}
