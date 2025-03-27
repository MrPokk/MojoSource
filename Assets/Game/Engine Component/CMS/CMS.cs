using System;
using System.Collections.Generic;
using Engin.Utility;
using Game.CMS_Content.Card;

public class CMS : BaseInteraction, IInitInMain
{

    public override Priority PriorityInteraction { get => Priority.FIRST_TASK; }

    private static List<CMSEntity> CMSEntities;

    public void Init()
    {
        CMSEntities = new List<CMSEntity>();
        FindAll();
    }


    public static T TryAddValue<T>() where T : CMSEntity, new()
    {
        T NewModel = new T();
        CMSEntities.Add(NewModel);
        
        return NewModel;
    }
    
    public static void Add<T>() where T : CMSEntity, new()
    {
        T NewModel = new T();
        CMSEntities.Add(NewModel);
    }

    private static void FindAll()
    {
        var ListCMSEntity = ReflectionUtility.FindAllImplement<CMSEntity>();
        foreach (var Element in ListCMSEntity)
        {
            CMSEntities.Add(Activator.CreateInstance(Element) as CMSEntity);
        }
    }

    public static void TryGetComponent<CMSEntityType, ComponentType>(out ComponentType refComponent) where ComponentType : class, IComponent where CMSEntityType : CMSEntity
    {
        var Entity = Get<CMSEntityType>();
        Entity.Get<ComponentType>(out var componentType);
        refComponent = componentType;
    }

    public static T Get<T>() where T : CMSEntity
    {
        foreach (var Element in CMSEntities)
        {
            if (Element is T ElementData)
                return ElementData;
        }
        throw new Exception("CMSEntity not found");
    }
    public static List<T> GetAll<T>() where T : CMSEntity
    {
        List<T> list = new List<T>();
        foreach (var Element in CMSEntities)
        {
            if (Element is T ElementData)
                list.Add(ElementData);
        }
        if (list.Count == 0)
            throw new Exception("CMSEntity not found");
        return list;
    }
}
