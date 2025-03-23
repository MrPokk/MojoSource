using Engin.Utility;
using UnityEngine;

public class Player : CMSEntity
{

    private Vector2Int _position;

    public Player()
    {
        Define(out ViewComponent viewComponent).Prefab = Resources.Load<GameObject>("Prefabs/Player");
        Define(out TransformComponent transformComponent);
        transformComponent.PositionInGrid = new Vector2Int(0, 0);
        
        RegisterComponents(viewComponent);
    }

    public override void RegisterComponents(params IComponent[] components)
    {
        Components.AddRange(components);
    }

}

public struct ViewComponent : IComponent
{
    public GameObject Prefab;
}

public struct TransformComponent : IComponent
{
    public Vector2Int PositionInGrid;
    public Transform Transform;
}

