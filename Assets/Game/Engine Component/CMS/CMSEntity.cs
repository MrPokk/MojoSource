using System;
using System.Collections.Generic;
using Engin.Utility;
using Game.CMS_Content;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CMSEntity
{
    private readonly HashSet<IComponent> _components = new HashSet<IComponent>();

    public Type ID { get => GetType(); }

    public void GetComponent<T>(out T refComponent) where T : class, IComponent
    {
        refComponent = _components.FirstOrDefault(c => c is T) as T;
    }

    protected void RemoveComponent<T>() where T : class, IComponent
    {
        GetComponent<T>(out T refComponent);
        _components.Remove(refComponent);
    }

    public ModelView GetView()
    {
        foreach (var component in _components)
        {
            if (component is ViewComponent viewComponent)
                return viewComponent.ViewModel;
        }
        return null;
    }

    public Vector2 GetViewPosition2D()
    {
        return new(GetView().transform.position.x, GetView().transform.position.y);
    }
    
    
    public Vector3 GetViewPosition3D()
    {
        return new(GetView().transform.position.x, GetView().transform.position.y,0);
    }
    
    protected void Define<T>(out T component) where T : IComponent, new()
    {
        component = new T();
        _components.Add(component);
    }
}
