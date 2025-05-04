using System;
using System.Collections.Generic;
using Engin.Utility;

public class CMS : BaseInteraction, IInitInMain
{
    public override Priority PriorityInteraction { get => Priority.FIRST_TASK; }

    private static HashSet<CMSEntity> _cmsEntities;

    public void Init()
    {
        _cmsEntities = new HashSet<CMSEntity>();

        FindAllEntity();
    }

    public static void Add(CMSEntity entity)
    {
        _cmsEntities.Add(entity);
    }
    private static void FindAllEntity()
    {
        var listCms = ReflectionUtility.FindAllImplement<CMSEntity>();
        foreach (var element in listCms)
        {
            _cmsEntities.Add(Activator.CreateInstance(element) as CMSEntity);
        }
    }

    public static void TryGetComponent<TCmsEntityType, TComponentType>(out TComponentType refComponent) where TComponentType : class, IComponent where TCmsEntityType : CMSEntity
    {
        var entity = Get<TCmsEntityType>();
        entity.GetComponent<TComponentType>(out var componentType);
        refComponent = componentType;
    }

    public static T Get<T>() where T : CMSEntity
    {
        foreach (var element in _cmsEntities)
        {
            if (element is T elementData)
                return elementData;
        }
        throw new Exception("CMSEntity not found");
    }
    public static List<T> GetAll<T>() where T : CMSEntity
    {
        var list = new List<T>();
        foreach (var element in _cmsEntities)
        {
            if (element is T elementData)
                list.Add(elementData);
        }
        if (list.Count == 0)
            throw new Exception("CMSEntity not found");
        return list;
    }
}
