using Engin.Utility;
using System;
using UnityEngine;

public class MouseController : BaseInteraction, IEnterInUpdate
{
    public override Priority PriorityInteraction { get => Priority.High; }

    private Vector2 _mousePose { get => GameData<Main>.Boot.MainCamera.ScreenToWorldPoint(Input.mousePosition); }

    public static event Action<Vector3> OnClickDown;
    public static event Action<Vector3> OnClickPressing;
    public static event Action<Vector3> OnClickUp;

    public void Update(float TimeDelta)
    {
        InputMouseClick();
    }
    private void InputMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClickDown?.Invoke(_mousePose);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnClickUp?.Invoke(_mousePose);
        }
        else if (Input.GetMouseButton(0))
        {
            OnClickPressing?.Invoke(_mousePose);
        }
 

    }
}
