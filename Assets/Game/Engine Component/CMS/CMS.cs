using System;
using System.Collections.Generic;
using Engin.Utility;

public class CMS : BaseInteraction, IInitInMain
{
    public override Priority PriorityInteraction { get => Priority.FIRST_TASK; }

    private static HashSet<CMSEntity> CMSEntities;

    public void Init()
    {
        CMSEntities = new HashSet<CMSEntity>();

        FindAllEntity();
    }

    public static void Add(CMSEntity Entity)
    {
        CMSEntities.Add(Entity);
    }
    private static void FindAllEntity()
    {
        var ListCMS = ReflectionUtility.FindAllImplement<CMSEntity>();
        foreach (var Element in ListCMS)
        {
            CMSEntities.Add(Activator.CreateInstance(Element) as CMSEntity);
        }
    }

    public static void TryGetComponent<CMSEntityType, ComponentType>(out ComponentType refComponent) where ComponentType : class, IComponent where CMSEntityType : CMSEntity
    {
        var Entity = Get<CMSEntityType>();
        Entity.GetComponent<ComponentType>(out var componentType);
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
