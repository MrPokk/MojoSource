using Game.CMS_Content;
using System.Collections.Generic;
using UnityEngine;
public abstract class CMSManager : CMSEntity
{
    private Dictionary<GameObject, CMSEntity> LoadedEntity { get; set; } = new Dictionary<GameObject, CMSEntity>();
    protected abstract void SpawnEntity<T>() where T : CMSEntity, new();
    public virtual void DestroyEntity(in GameObject ID)
    {
        LoadedEntity.Remove(ID);
        Object.Destroy(ID);
    }
    
    public IReadOnlyDictionary<GameObject, CMSEntity> GetEntities()
    {
        return LoadedEntity;
    }

    protected void Create<T>(out GameObject ID) where T : CMSEntity, new()
    {
        T NewEntity = new T();

        NewEntity.GetComponent<ViewComponent>(out var view);

        view.ViewModel.name = $"{NewEntity.ID}";
        GameData<Main>.Boot.InstantiateCMSEntity(view);

        ID = view.ViewModel.gameObject;
        LoadedEntity.Add(view.ViewModel.gameObject, NewEntity);
    }
    
    public T GetEntityByID<T>(in GameObject ID) where T : CMSEntity
    {
        return GetEntityByID(ID) as T;
    }

    public CMSEntity GetEntityByID(in GameObject ID)
    {
        var Entity = LoadedEntity.TryGetValue(ID, out CMSEntity value);
        return Entity ? value : null;
    }

}
