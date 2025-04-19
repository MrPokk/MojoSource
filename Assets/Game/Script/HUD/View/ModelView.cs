using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModelView : MonoBehaviour
{
    public Type ID { get => GetType(); }
    
    public CMSEntity GetModel()
    {
        foreach (var CMSManagers in CMS.GetAll<CMSManager>())
        {
            var Entity = CMSManagers.GetEntityByID(gameObject);
            if (Entity != null)
                return Entity;
        }
        return null;
    }

}
