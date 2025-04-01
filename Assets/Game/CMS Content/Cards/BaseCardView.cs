using Game.Script.GlobalComponent;
using UnityEngine;

namespace Game.CMS_Content.Card
{
    public class BaseCardView : DraggableObject<BaseCardView>
    {
        protected override void Drop(Vector3 mousePosition)
        {
            base.Drop(mousePosition);
            GameData<Main>.Boot.HandCards.Add(this);
        }
        protected override void Drag(Vector3 mousePosition)
        {
            base.Drag(mousePosition);
            GameData<Main>.Boot.HandCards.Remove(this);
        }
    }
}
