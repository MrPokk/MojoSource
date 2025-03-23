using UnityEngine;
public struct GridNode
{
    public GridNode(Vector2Int index) : this()
    {
        Index = index;
        IndexParent = Vector2Int.one * -1; // Multiply by -1 because there may not be a leading node for the node.

        GCost = int.MaxValue;
        HCost = 0;
    }

    public Vector2Int Index;
    public Vector2Int IndexParent;

    public int GCost;
    public int HCost;
    public int FCost => GCost + HCost;

}
