using Game.CMS_Content.Entity.Type;
using Game.CMS_Content.Entitys.Components;
using Game.Script.Global_Interactions;
using System.Collections;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Player.Interactions
{
    public class MoveInteraction : BaseInteraction, IEnterInStart, IExitInGame
    {
        private bool _isSelect;
        private ModelView _selectView;

        public void Start()
        {
            RaycastInteraction.ReactionRaycast += ReactionOnRaycast;
        }
        public void Stop()
        {
            RaycastInteraction.ReactionRaycast -= ReactionOnRaycast;
        }

        private void ReactionOnRaycast(ModelView objectRaycast)
        {

            if (_isSelect || objectRaycast is not PlayerView)
                return;

            _isSelect = true;
            _selectView = objectRaycast;

            MouseInteraction.OnClickDown += SelectingGridNode;
        }
        private void SelectingGridNode(Vector2 mousePosition)
        {
            var PlayerModel = _selectView.GetModel();
            PlayerModel.GetComponent<MoveComponent>(out var moveComponent);

            if (GridUtility.GridMousePosition == PlayerModel.GetViewPosition2D().normalized)
                return;

            moveComponent.MoveMethod?.Invoke(GridUtility.GridMousePosition);

            _isSelect = false;
            MouseInteraction.OnClickDown -= SelectingGridNode;
        }
    }
}
