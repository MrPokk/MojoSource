using Engin.Utility;
using Game.CMS_Content;
using Game.CMS_Content.Card;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour, IMain
{

    private Interaction _interact;

    [SerializeField] private GridView gridView;

    public GridController GridController { get; private set; }
    public BaseCardController CardController { get; private set; }


    public HandCards HandCards;

    public Camera MainCamera { get; private set; }
    
    public void StartGame()
    {
        _interact = new Interaction();
        _interact.Init();

        GameData<Main>.Boot = this;

        GridController = new GridController(new(10, 10), 1, gridView);
        CardController = new BaseCardController();
        
        MainCamera = Camera.main;

        var Init = _interact.FindAll<IInitInMain>();
        foreach (var Element in Init)
        {
            Element.Init();
        }

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

    public GameObject InstantiateCMSEntity(in ViewComponent SetView, Vector3 Position = default, Quaternion Rotation = default)
    {
        if (SetView.Prefab == default)
            throw new NullReferenceException("The Entity is null");

        var Instanse = Instantiate(SetView.Prefab, Position, Rotation);
        return SetView.Prefab = Instanse;
    }

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

public abstract class SystemInit : BaseInteraction, IEnterInStart
{
    protected abstract void Init();

    public void Start()
    {
        Init();
    }
}
