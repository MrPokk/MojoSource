using UnityEngine;

public class GridView : ModelView
{
    [SerializeField]
    private GameObject _node;
    private GridController _gridController;

    public void Init(Vector2Int size, GridController gridController, float cellSize)
    {
        _gridController = gridController;
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var node = Instantiate(_node, transform);
                node.transform.position += new Vector3(x, y) * cellSize + (new Vector3(cellSize, cellSize) / 2);
                node.transform.localScale = new Vector3(cellSize, cellSize, 0);
            }
        }
    }
}
