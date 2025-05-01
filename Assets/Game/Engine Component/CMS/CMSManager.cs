using Game.CMS_Content;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Engine_Component.CMS
{
    public abstract class CMSManager : CMSEntity
    {
        private readonly Dictionary<GameObject, CMSEntity> _loadedEntity = new Dictionary<GameObject, CMSEntity>();
        protected abstract void SpawnEntity<T>() where T : CMSEntity, new();
    
        public virtual void DestroyEntity(in GameObject ID)
        {
            _loadedEntity.Remove(ID);
            Object.Destroy(ID);
        }
    
        public IReadOnlyDictionary<GameObject, CMSEntity> GetEntities()
        {
            return _loadedEntity;
        }

        protected void Create<T>(out GameObject ID) where T : CMSEntity, new()
        {
            T newEntity = new T();

            newEntity.GetComponent<ViewComponent>(out var view);

            view.ViewModel.name = $"{newEntity.ID}";
            GameData<Main>.Boot.InstantiateCMSEntity(view);

            ID = view.ViewModel.gameObject;
            _loadedEntity.Add(view.ViewModel.gameObject, newEntity);
        }
    
        public T GetEntityByID<T>(in GameObject ID) where T : CMSEntity
        {
            return GetEntityByID(ID) as T;
        }

        public CMSEntity GetEntityByID(in GameObject ID)
        {
            var entity = _loadedEntity.TryGetValue(ID, out CMSEntity value);
            return entity ? value : null;
        }

    }
}
