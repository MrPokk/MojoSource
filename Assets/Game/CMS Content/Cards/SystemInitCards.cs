using Game.CMS_Content.Cards.Type;

namespace Game.CMS_Content.Cards
{
    public class SystemInitCards : SystemInit
    {
        protected override void Init()
        {
            var controller = CMS.Get<BaseCardController>();

            controller.GiveCardInHand<DamageCard>();
            controller.GiveCardInHand<DamageCard>();
            controller.GiveCardInHand<DamageCard>();
        }
    }
}
