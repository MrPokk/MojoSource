using System;
using System.Collections.Generic;
using Engin.Utility;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CMSEntity
{
    public HashSet<IComponent> Components { get; private set; } = new HashSet<IComponent>();
    protected void Define<T>(out T component) where T : IComponent, new()
    {
        component = new T();
        RegisterComponents(component);
    }
    private void RegisterComponents(params IComponent[] refComponent)
    {
        Components.AddRange(refComponent);
    }

    public void Get<T>(out T refComponent) where T : class, IComponent
    {
        refComponent = Components.FirstOrDefault((c) => c is T) as T;

        if (refComponent == null)
            throw new Exception("Component not found");
    }
}
