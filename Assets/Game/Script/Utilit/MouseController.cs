using Engin.Utility;
using System;
using UnityEngine;

public class MouseController : BaseInteraction, IEnterInUpdate
{
    public override Priority PriorityInteraction { get => Priority.High; }

    public static event Action OnClick;
    
    public void Update(float TimeDelta)
    {
        InputMouseClick();
    }
    private void InputMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick?.Invoke();
        }
    }
}
