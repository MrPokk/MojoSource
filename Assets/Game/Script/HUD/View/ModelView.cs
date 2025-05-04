using Game.CMS_Content.Cards;
using Game.Engine_Component.CMS;
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
            var Entity = CMSManagers.GetEntityByID(this);
            if (Entity != null)
                return Entity;
        }
        return null;
    }
    
    public T GetModel<T>() where T : CMSEntity
    {
        foreach (var CMSManagers in CMS.GetAll<CMSManager>())
        {
            var Entity = CMSManagers.GetEntityByID(this);
            if (Entity != null)
                return Entity as T;
        }
        return null;
    }

}
