using Engin.Utility;
using System;
using UnityEngine;
public class Main : MonoBehaviour, IMain
{


    private Interaction _interact;

    [SerializeField] private GridView gridView;

    public GridController GridController { get; private set; }


    public void StartGame()
    {

        _interact = new Interaction();
        _interact.Init();

        GameData<Main>.Boot = this;

        GridController = new GridController(new(10, 10), 1, gridView);
        
        var Starts = _interact.FindAll<IEnterInStart>();

        foreach (var Element in Starts)
        {
            Element.Start();
        }

        GameData<Main>.IsStartGame = true;
        
    }

    public void UpdateGame(float TimeDelta)
    {
        var Update = _interact.FindAll<IEnterInUpdate>();
        foreach (var Element in Update)
        {
            Element.Update(TimeDelta);
        }
    }

    public void PhysicUpdateGame(float TimeDelta)
    { }

    private void Update()
    {
        UpdateGame(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        PhysicUpdateGame(Time.deltaTime);
    }
    private void Awake()
    {
        StartGame();
    }
}
