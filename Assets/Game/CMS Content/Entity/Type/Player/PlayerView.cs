using Game.CMS_Content.Entity.Components;
using Game.Script.GlobalComponent.Interface;
using System.Collections;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Game.CMS_Content.Entity.Type
{
    public class PlayerView : ModelView, IRaycasting
    {
        private bool _isSelect;

        private void OnEnable()
        {
            MouseController.OnClickDown += CheckMouseClick;
        }
        private void OnDestroy()
        {
            MouseController.OnClickDown -= CheckMouseClick;
        }

        private void CheckMouseClick(Vector3 mousePosition)
        {
            var mRaycastObject = IRaycasting.TryGetRaycastObject<PlayerView>(mousePosition, out var view);
            if (!mRaycastObject && !_isSelect)
                return;

            if (!_isSelect)
            {
                _isSelect = true;
                GameData<Main>.Corotine.StartCoroutine(SelectPointToMove());
            }
            else
            {
                
                var PlayerModel = CMS.Get<BaseEntityController>().GetEntityByID<PlayerModel>(gameObject);
                PlayerModel.Get<MoveComponent>(out var moveComponent);
                moveComponent.MoveMethod?.Invoke(mousePosition);
            }
        }

        private IEnumerator SelectPointToMove()
        {
            yield break;
        }
    }
}
