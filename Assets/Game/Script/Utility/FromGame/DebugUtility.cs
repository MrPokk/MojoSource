using Game.CMS_Content.Cards;
using Game.CMS_Content.Cards.Type;
using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if DEBUG || UNITY_EDITOR
public class DebugUtility : MonoBehaviour
{

    public static event Action DebugGizmos;
    
    private static DebugUtility _instance = null;
    public static DebugUtility Instance {
        get {
            if (_instance != null)
                return _instance;

            var FindDebugObject = Object.FindFirstObjectByType<DebugUtility>();

            if (FindDebugObject == null)
                _instance = new GameObject($"[Debug]").AddComponent<DebugUtility>();

            return _instance = FindDebugObject;
        }
    }
    

    private void OnDrawGizmos()
    {
        DebugGizmos?.Invoke();
    }
}
  #endif

public class DebugInteraction : BaseInteraction, IEnterInUpdate
{
    public void Update(float TimeDelta)
    {
        if (Input.GetKeyDown(KeyCode.K))
            CMS.Get<BaseCardController>().GiveCardInHand<ArsonCard>();
    }
}