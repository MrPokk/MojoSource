using Game.CMS_Content.Entity.Components;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Game.CMS_Content.Entity.System
{
    public class MoveSystem : BaseInteraction, IEnterInStart
    {
        public void Start()
        {
            MouseController.OnClickDown += MoveAll;
        }
        private void MoveAll(Vector3 mousePosition)
        {
            
            foreach (var EntityModel in CMS.Get<BaseEntityController>()._loadedEntity)
            {
                EntityModel.Get<MoveComponent>(out var moveComponent);
                moveComponent.MoveMethod?.Invoke(mousePosition);
            }
        }

    }
}
