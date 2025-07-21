using Engin.Utility;
using Game.Engine_Component.CMS;
using System.Linq;
using UnityEngine;

namespace Game.Script.Utility.FromGame
{
    public static class TransformUtility
    {
        public static CMSEntity FindToNearestComponent<TComponent>(ModelView fromModel) where TComponent : class, IComponent
        {
            CMSEntity nearestEntity = null;

            var nearestDistance = float.MaxValue;
            var fromModelPosition = fromModel.transform.position;
            
            var cachedManagers = CMS.GetAll<CMSManager>().ToList();


            foreach (var cmsManager in cachedManagers)
            {
                var cachedEntities = cmsManager.GetEntities().ToList();

                foreach (var entity in cachedEntities)
                {
                    var entityPosition = entity.Key.transform.position;
                    var distanceToModel = Vector2.Distance(fromModelPosition, entityPosition);

                    entity.Value.GetComponent(out TComponent component);
                    if (!(distanceToModel < nearestDistance) || component == null || component.GetType() != typeof(TComponent))
                        continue;

                    nearestDistance = distanceToModel;
                    nearestEntity = entity.Value;
                }
            }
            return nearestEntity;
        }

        public static CMSEntity FindToNearest<TEntity>(ModelView fromModel) where TEntity : CMSEntity
        {
            if (!fromModel)
                return null;

            var nearestDistance = float.MaxValue;
            var fromModelPosition = fromModel.transform.position;
            CMSEntity nearestEntity = null;

            var cachedManagers = CMS.GetAll<CMSManager>().ToList();

            foreach (var cmsManager in cachedManagers)
            {
                var cachedEntities = cmsManager.GetEntities().ToList();

                foreach (var entity in cachedEntities)
                {
                    if (entity.Value is not TEntity)
                        continue;

                    var entityPosition = entity.Key.transform.position;
                    var distanceToModel = Vector2.Distance(fromModelPosition, entityPosition);

                    if (distanceToModel < nearestDistance)
                    {
                        nearestDistance = distanceToModel;
                        nearestEntity = entity.Value;
                    }
                }
            }

            return nearestEntity;
        }

        public static CMSEntity FindToNearest(ModelView fromModel)
        {
            CMSEntity nearestEntity = null;

            var nearestDistance = float.MaxValue;
            var fromModelPosition = fromModel.transform.position;

            var cachedManagers = CMS.GetAll<CMSManager>().ToList();

            
            foreach (var cmsManager in cachedManagers)
            {

                var cachedEntities = cmsManager.GetEntities().ToList();
                
                foreach (var entity in cachedEntities)
                {
                    var entityPosition = entity.Key.transform.position;
                    var distanceToModel = Vector2.Distance(fromModelPosition, entityPosition);

                    if (!(distanceToModel < nearestDistance))
                        continue;

                    nearestDistance = distanceToModel;
                    nearestEntity = entity.Value;

                }
            }

            return nearestEntity;
        }

    }
}
