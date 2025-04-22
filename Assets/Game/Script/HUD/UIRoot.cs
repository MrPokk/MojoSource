using UnityEngine;

public class UIRoot : MonoBehaviour
{
    public void NextTurn()
    {
        foreach (var NextTurnElement in InteractionCache<IEnterInNextTurn>.AllInteraction)
        {
            NextTurnElement.UpdateTurn();
        }
    }
    
}
