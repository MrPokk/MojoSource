using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMonoBehaviour : MonoBehaviour
{

    private Vector3 _startPosition;
    private void OnEnable()
    {
        MouseController.OnClick += Move;
    }
    private void OnDestroy()
    {
        MouseController.OnClick -= Move;
    }

    private void Start()
    {
        _startPosition = GameData<Main>.Boot.GridController.Grid.GetWorldPose(transform.position);
        transform.position = new Vector3(_startPosition.x, _startPosition.y) + transform.localScale / 2;
    }

    private void Move()
    {
        Camera Camera = Camera.main;
        Vector2 MousePose = Camera.ScreenToWorldPoint(Input.mousePosition);

        var MousePoseInGrid = GameData<Main>.Boot.GridController.Grid.GetGridPose(MousePose);

        var ConverPositionPlayer = GameData<Main>.Boot.GridController.Grid.GetGridPose(transform.position);

        var Path = AStar.PathFindInGrid(ConverPositionPlayer, MousePoseInGrid, GameData<Main>.Boot.GridController.Grid.Array);

        if (Path != null)
            StartCoroutine(StepOnGrid(Path));
    }

    private IEnumerator StepOnGrid(List<Vector2Int> path)
    {
        foreach (var Element in path)
        {
            transform.position = GameData<Main>.Boot.GridController.Grid.GetWorldPose(Element) + transform.localScale / 2;
            yield return new WaitForSeconds(1);
        }

        _startPosition = GameData<Main>.Boot.GridController.Grid.GetWorldPose(path.LastOrDefault());
    }
}
