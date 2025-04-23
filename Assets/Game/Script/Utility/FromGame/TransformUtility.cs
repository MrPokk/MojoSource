using Engin.Utility;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Script.Utility
{
    public sealed class TransformUtility
    {

        public static CMSEntity FindToNearest<TEntity, TComponent>(ModelView fromModel) where TEntity : CMSEntity where TComponent : class, IComponent
        {
            CMSEntity NearestEntity = null;

            float NearestDistance = float.MaxValue;
            Vector3 FromModelPosition = fromModel.transform.position;
            
            foreach (var CMSManager in CMS.GetAll<CMSManager>())
            {

                foreach (var Entity in CMSManager.GetEntities())
                {
                    var EntityPosition = Entity.Key.transform.position;
                    var DistanceToModel = Vector2.Distance(FromModelPosition, EntityPosition);

                    Entity.Value.GetComponent(out TComponent component);
                    if (!(DistanceToModel < NearestDistance) || component == null || component.GetType() != typeof(TComponent))
                        continue;
                    
                    NearestDistance = DistanceToModel;
                    NearestEntity = Entity.Value;
                }
            }
            return NearestEntity;
        }
        
        public static CMSEntity FindToNearest<T>(ModelView fromModel) where T : CMSEntity
        {
            CMSEntity NearestEntity = null;

            float NearestDistance = float.MaxValue;
            Vector3 FromModelPosition = fromModel.transform.position;
            
            foreach (var CMSManager in CMS.GetAll<CMSManager>())
            {

                foreach (var Entity in CMSManager.GetEntities())
                {
                    var EntityPosition = Entity.Key.transform.position;
                    var DistanceToModel = Vector2.Distance(FromModelPosition, EntityPosition);
                    
                    if (!(DistanceToModel < NearestDistance) || Entity.Value.GetType() != typeof(T))
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

                foreach (var Entity in CMSManager.GetEntities())
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
