using Engin.Utility;
using UnityEngine;

namespace Game.CMS_Content.Cards.Type
{
    public class ArsonCard : BaseCardModel
    {
        public ArsonCard()
        {
            Define(out ViewComponent viewComponent);
            viewComponent.LoadView<BaseCardView>(PathResources.PREFABS);

            CardComponentBase.PriorityCardComponent.Priority = Priority.High;
            CardComponentBase.ActionCardComponent.AbilityCard = () => { Debug.Log("dssa"); };
        }
    }
}
