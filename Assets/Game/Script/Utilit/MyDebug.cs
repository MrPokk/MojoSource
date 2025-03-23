using Engin.Utility;
using System;
using UnityEngine;


public class MyDebug : MonoBehaviour
{

    private Main Main => GameData<Main>.Boot;
    public void Start()
    {
        MouseController.OnClick += FindPath;
    }

    private void FindPath()
    {
        var NodePathIndexs = AStar.PathFindInGrid(new Vector2Int(0, 0), new Vector2Int(5, 5), Main.GridController.Grid.Array);
    }
}
