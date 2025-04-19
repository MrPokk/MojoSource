using Engin.Utility;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Game.Script.Utility
{
    public sealed class TransformUtility
    {
        public static CMSEntity FindToNearest<T>(ModelView fromModel) where T : class, IComponent
        {
            CMSEntity NearestEntity = null;

            float NearestDistance = float.MaxValue;
            Vector3 FromModelPosition = fromModel.transform.position;

            foreach (var CMSManager in CMS.GetAll<CMSManager>())
            {

                foreach (var Entity in CMSManager.LoadedEntity)
                {
                    var EntityPosition = Entity.Key.transform.position;
                    var DistanceToModel = Vector2.Distance(FromModelPosition, EntityPosition);

                    Entity.Value.GetComponent(out T component);
                    if (!(DistanceToModel < NearestDistance) || component == null || component.GetType() != typeof(T))
                        continue;
                    
                    NearestDistance = DistanceToModel;
                    NearestEntity = Entity.Value;
                }
            }
            return NearestEntity;
        }

        public static CMSEntity FindToNearest(ModelView fromModel)
        {
            CMSEntity NearestEntity = null;

            float NearestDistance = float.MaxValue;
            Vector3 FromModelPosition = fromModel.transform.position;

            foreach (var CMSManager in CMS.GetAll<CMSManager>())
            {

                foreach (var Entity in CMSManager.LoadedEntity)
                {
                    var EntityPosition = Entity.Key.transform.position;
                    var DistanceToModel = Vector2.Distance(FromModelPosition, EntityPosition);

                    if (!(DistanceToModel < NearestDistance))
                        continue;

                    NearestDistance = DistanceToModel;
                    NearestEntity = Entity.Value;

                }
            }

            return NearestEntity;
        }

    }
}
