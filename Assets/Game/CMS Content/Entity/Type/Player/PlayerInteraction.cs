using Game.CMS_Content.Entity.Components;
using Game.Script.GlobalComponent.Interface;
using System.Collections;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Game.CMS_Content.Entity.Type
{
    public class PlayerInteraction : BaseInteraction, IEnterInStart, IExitInGame
    {
        private bool _isSelect;
        private PlayerView _selectView;

        public void Start()
        {
            MouseController.OnClickDown += ReactionOnClick;
        }
        public void Stop()
        {
            MouseController.OnClickDown -= ReactionOnClick;
        }

        private void ReactionOnClick(Vector3 mousePosition)
        {
            var RaycastObject = Raycasting.TryGetRaycastObject<PlayerView>(mousePosition, out var view);

            switch (RaycastObject)
            {
                case true when !_isSelect:
                    _isSelect = true; _selectView = view;
                    return;
                
                case true when _isSelect:
                    _isSelect = false;
                    break;
            }

            if (_isSelect)
                SelectingGridNode(GridUtility.GridMousePosition);

        }
        

        private void SelectingGridNode(Vector2Int movePositionInGrid)
        {
            var PlayerModel = CMS.Get<BaseEntityController>().GetEntityByID<PlayerModel>(_selectView.gameObject);
            PlayerModel.Get<MoveComponent>(out var moveComponent);
            
            moveComponent.MoveMethod?.Invoke(movePositionInGrid);

            _isSelect = false;
        }
    }
}
