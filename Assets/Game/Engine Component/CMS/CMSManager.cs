using Game.CMS_Content;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Game.Engine_Component.CMS
{
    public abstract class CMSManager : CMSEntity
    {
        private readonly Dictionary<ModelView, CMSEntity> _loadedEntity = new Dictionary<ModelView, CMSEntity>();
        protected abstract void SpawnEntity<T>() where T : CMSEntity, new();

        public virtual void DestroyEntity(in ModelView ID)
        {
            _loadedEntity.Remove(ID);
            Object.Destroy(ID.gameObject);
        }

        public IReadOnlyDictionary<ModelView, CMSEntity> GetEntities()
        {
            return _loadedEntity;
        }

        protected void Create<T>(out ModelView ID) where T : CMSEntity, new()
        {
            var newEntity = new T();

            newEntity.GetComponent<ViewComponent>(out var view);
            if (view == null)
            {
                ID = null;
                return;
            }

            view.ViewModel.name = $"{newEntity.ID}";
            GameData<Main>.Boot.InstantiateCMSEntity(view);

            ID = view.ViewModel;
            _loadedEntity.Add(view.ViewModel, newEntity);
        }
        protected void Create(CMSEntity entity, out ModelView ID)
        {
            var typeEntity = entity.ID;
            if (typeEntity.IsAbstract)
            {
                ID = null;
                return;
            }

            var newObject = Activator.CreateInstance(typeEntity);
            if (newObject is not CMSEntity newEntity)
            {
                ID = null;
                return;
            }

            newEntity.GetComponent<ViewComponent>(out var view);
            if (view == null)
            {
                ID = null;
                return;
            }

            view.ViewModel.name = $"{newEntity.ID}";
            GameData<Main>.Boot.InstantiateCMSEntity(view);

            ID = view.ViewModel;
            _loadedEntity.Add(view.ViewModel, newEntity);
        }
        public T GetEntityByID<T>(in ModelView ID) where T : CMSEntity
        {
            return GetEntityByID(ID) as T;
        }

        public CMSEntity GetEntityByID(in ModelView ID)
        {
            var entity = _loadedEntity.TryGetValue(ID, out CMSEntity value);
            return entity ? value : null;
        }
    }
}
