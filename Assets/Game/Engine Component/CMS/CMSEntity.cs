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
        refComponent = _components.FirstOrDefault((c) => c is T) as T;
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




    protected void Define<T>(out T component) where T : IComponent, new()
    {
        component = new T();
        RegisterComponents(component);
    }
    private void RegisterComponents(params IComponent[] refComponent)
    {
        _components.AddRange(refComponent);
    }


}
