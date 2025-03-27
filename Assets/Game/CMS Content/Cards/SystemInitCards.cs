using Game.CMS_Content.Card.Type;


namespace Game.CMS_Content.Card
{
    public class SystemInitCards : SystemInit
    {
        protected override void Init()
        {
            var Controller = GameData<Main>.Boot.CardController;

            Controller.GiveCardInHand<ArsonCard>();
            Controller.GiveCardInHand<ArsonCard>();
            Controller.GiveCardInHand<ArsonCard>();

        }
    }
}
