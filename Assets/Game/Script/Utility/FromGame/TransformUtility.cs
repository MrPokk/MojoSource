using Engin.Utility;
using Game.Engine_Component.CMS;
using UnityEngine;

namespace Game.Script.Utility.FromGame
{
    public static class TransformUtility
    {
        public static CMSEntity FindToNearest<TEntity, TComponent>(ModelView fromModel) where TEntity : CMSEntity where TComponent : class, IComponent
        {
            CMSEntity nearestEntity = null;

            var nearestDistance = float.MaxValue;
            var fromModelPosition = fromModel.transform.position;

            foreach (var cmsManager in CMS.GetAll<CMSManager>())
            {
                foreach (var entity in cmsManager.GetEntities())
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

        public static CMSEntity FindToNearest<T>(ModelView fromModel) where T : CMSEntity
        {
            CMSEntity nearestEntity = null;

            if (fromModel == null)
                return null;

            var nearestDistance = float.MaxValue;
            var fromModelPosition = fromModel.transform.position;

            foreach (var cmsManager in CMS.GetAll<CMSManager>())
            {

                foreach (var entity in cmsManager.GetEntities())
                {
                    var entityPosition = entity.Key.transform.position;
                    var distanceToModel = Vector2.Distance(fromModelPosition, entityPosition);

                    if (!(distanceToModel < nearestDistance) || entity.Value.GetType() != typeof(T))
                        continue;

                    nearestDistance = distanceToModel;
                    nearestEntity = entity.Value;
                }
            }
            return nearestEntity;
        }

        public static CMSEntity FindToNearest(ModelView fromModel)
        {
            CMSEntity nearestEntity = null;

            var nearestDistance = float.MaxValue;
            var fromModelPosition = fromModel.transform.position;

            foreach (var cmsManager in CMS.GetAll<CMSManager>())
            {

                foreach (var entity in cmsManager.GetEntities())
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
