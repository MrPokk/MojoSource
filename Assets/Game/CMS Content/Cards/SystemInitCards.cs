using Game.CMS_Content.Cards.Type;

namespace Game.CMS_Content.Cards
{
    public class SystemInitCards : SystemInit
    {
        protected override void Init()
        {
            var Controller = CMS.Get<BaseCardController>();

            Controller.GiveCardInHand<ArsonCard>();
            Controller.GiveCardInHand<ArsonCard>();
            Controller.GiveCardInHand<ArsonCard>();
        }
    }
}
