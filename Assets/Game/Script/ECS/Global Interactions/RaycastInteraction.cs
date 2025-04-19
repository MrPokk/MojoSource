using Game.CMS_Content;
using System;
using UnityEngine;

namespace Game.Script.Global_Interactions
{
    public class RaycastInteraction : BaseInteraction, IEnterInStart, IExitInGame
    {

        public static event Action<ModelView> ReactionRaycast;

        public void Start()
        {
            MouseInteraction.OnClickDown += Raycasting;
        }

        public void Stop()
        {
            MouseInteraction.OnClickDown -= Raycasting;
        }

        private void Raycasting(Vector2 mousePosition)
        {
            var hitPoint = Physics2D.Raycast(mousePosition, Vector2.zero);
            
            if (!hitPoint.collider || !hitPoint.collider.gameObject)
                return;

            hitPoint.collider.gameObject.TryGetComponent<ModelView>(out var viewRaycast);
            viewRaycast.GetModel().GetComponent<RaycastingComponent>(out var raycastingComponent);
            
            if (raycastingComponent != null && viewRaycast)
                ReactionRaycast?.Invoke(viewRaycast);
        }
    }
}
