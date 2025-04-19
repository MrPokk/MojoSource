#if DEBUG || UNITY_EDITOR
using Game.CMS_Content.Cards;
using Game.CMS_Content.Cards.Type;
using UnityEngine;
public class MyDebug : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            CMS.Get<BaseCardController>().GiveCardInHand<ArsonCard>();

        if (Input.GetKeyDown(KeyCode.P))
        {
            var PathTest = AStar.TryGetPathFind(new(0, 0), new(5, 5), GameData<Main>.Boot.GridController.Grid.Array);
            Debug.Log(PathTest.Count);
        }
    }
}
#endif
