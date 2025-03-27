using Engin.Utility;
using System;
using UnityEngine;

public class MouseController : BaseInteraction, IEnterInUpdate
{
    public override Priority PriorityInteraction { get => Priority.High; }

    private Vector3 _mousePose { get => GameData<Main>.Boot.MainCamera.ScreenToWorldPoint(Input.mousePosition); }

    public static event Action<Vector3> OnClick;

    public void Update(float TimeDelta)
    {
        InputMouseClick();
    }
    private void InputMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick?.Invoke(_mousePose);
        }
    }
}
