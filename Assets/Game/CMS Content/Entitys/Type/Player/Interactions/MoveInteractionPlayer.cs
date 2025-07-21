using Game.CMS_Content.Entitys.Components;
using Game.Script.Global_Interactions;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Player.Interactions
{
    public class MoveInteractionPlayer : BaseInteraction, IEnterInStart, IExitInGame
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
            var playerModel = _selectView.GetModel();
            playerModel.GetComponent<MoveComponent>(out var moveComponent);

            var mousePositionGrid = moveComponent.GridController.GridMousePosition;
            
            if (mousePositionGrid == playerModel.GetViewPosition2D().normalized)
                return;

            moveComponent.MoveMethod?.Invoke(mousePositionGrid);

            _isSelect = false;
            MouseInteraction.OnClickDown -= SelectingGridNode;
        }

    }
}
