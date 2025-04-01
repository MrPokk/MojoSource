using Game.CMS_Content.Card;
using Game.Script.GlobalComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Player
{


    public class EventPlayerController : MonoBehaviour
    {
        
        private bool _isWalking = false;
        private void OnEnable()
        {
            MouseController.OnClickDown += Move;
            DraggableObject<BaseCardView>.ReactionOnDrop += AddCard;
        }
        private void OnDestroy()
        {
            MouseController.OnClickDown -= Move;
            DraggableObject<BaseCardView>.ReactionOnDrop -= AddCard;
        }
        private void Move(Vector3 mousePosition)
        {
            
            var MousePoseInGrid = GridUtility.TryGetPositionInGrid(mousePosition, GameData<Main>.Boot.GridController.Grid);
            Vector2Int PlayerPoseInGrid = GridUtility.TryGetPositionInGrid(transform.position, GameData<Main>.Boot.GridController.Grid);

            var Path = AStar.TryGetPathFind(PlayerPoseInGrid, MousePoseInGrid, GameData<Main>.Boot.GridController.Grid.Array);

            if (Path != null && _isWalking == false)
                StartCoroutine(MakeByStep(Path));
        }
        
        private void AddCard(DraggableObject<BaseCardView> card)
        {
            
            Debug.Log("TEST");
            
            CMS.Get<EntityPlayer>().InsideCard.Add(card as BaseCardView);
            GameData<Main>.Boot.HandCards.Remove(card as BaseCardView);
            
            Debug.Log(CMS.Get<EntityPlayer>().InsideCard);
        }

        private IEnumerator MakeByStep(List<Vector2Int> path)
        {
            _isWalking = true;
            foreach (var Element in path)
            {
                transform.position = GameData<Main>.Boot.GridController.Grid.ConvertingPosition(Element) + transform.localScale / 2;
                yield return new WaitForSeconds(0.5f);
            }
            _isWalking = false;
        }
    }
}
