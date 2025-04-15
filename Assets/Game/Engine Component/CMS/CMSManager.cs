
    using Game.CMS_Content;
    using System.Collections.Generic;
    using UnityEngine;
    public abstract class CMSManager : CMSEntity
    {
        public Dictionary<GameObject, CMSEntity> LoadedEntity { get; private set; } = new Dictionary<GameObject, CMSEntity>();
        protected abstract void SpawnEntity<T>() where T : CMSEntity, new();
        protected void Create<T>(out GameObject ID) where T : CMSEntity, new()
        {
            T NewEntity = new T();

            NewEntity.Get<ViewComponent>(out var view);

            view.ViewModel.name = $"{NewEntity.ID}";
            GameData<Main>.Boot.InstantiateCMSEntity(view);

            ID = view.ViewModel.gameObject;
            LoadedEntity.Add(view.ViewModel.gameObject, NewEntity);
        }

        public T GetEntityByID<T>(in GameObject ID) where T : CMSEntity
        {
            var Entity = LoadedEntity.GetValueOrDefault(ID);
            if (Entity == null)
                Debug.LogError("ERROR: Incorrect MODEL type or ID");

            return Entity as T;
        }

    }
