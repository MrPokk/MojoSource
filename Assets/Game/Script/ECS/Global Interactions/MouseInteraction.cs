using Engin.Utility;
using System;
using System.Collections;
using UnityEngine;

public class MouseInteraction : BaseInteraction, IEnterInUpdate
{
    public override Priority PriorityInteraction => Priority.High; 

    public static Vector2 MousePose => GameData<Main>.Boot.MainCamera.ScreenToWorldPoint(Input.mousePosition); 
    
    public static event Action<Vector2> OnClickDown;
    public static event Action<Vector2> OnClickPressing;
    public static event Action<Vector2> OnClickUp;

    public void Update(float TimeDelta)
    {
        InputMouseClick();
    }
    private void InputMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
            OnClickDown?.Invoke(MousePose);
        
        else if (Input.GetMouseButtonUp(0))
            OnClickUp?.Invoke(MousePose);
        
        else if (Input.GetMouseButton(0))
            OnClickPressing?.Invoke(MousePose);
    }
}
