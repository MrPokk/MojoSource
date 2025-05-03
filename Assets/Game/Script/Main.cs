using Engin.Utility;
using Game.CMS_Content;
using Game.Script.ECS.Global_Interactions;
using Game.Script.Utility.FromGame;
using System;
using UnityEngine;
public class Main : MonoBehaviour, IMain
{
    private Interaction _interact;
    private TurnInteraction _turnInteraction;
    
    public GridView GridView;
    public HandCards HandCards;
    public UIRoot UIRoot;
    public GridController GridController { get; private set; }
    public Camera MainCamera { get; private set; }

    public void StartGame()
    {
        _interact = new Interaction();
        _interact.Init();

        GameData<Main>.Boot = this;


        GridController = new GridController(new(12, 5), 0.73f, GridView);

        MainCamera = Camera.main;

        var coroutine = new GameObject("[Coroutine]").AddComponent<CoroutineUtility>();
        GameData<Main>.Coroutine = new(coroutine);

        var init = _interact.FindAll<IInitInMain>();
        foreach (var element in init)
        {
            element.Init();
        }

        var starts = _interact.FindAll<IEnterInStart>();
        foreach (var element in starts)
        {
            element.Start();
        }
        
        _interact.FindAll<IEnterInUpdate>();
        _interact.FindAll<IExitInGame>();
        _interact.FindAll<IEnterInPhysicUpdate>();
        _interact.FindAll<IExitInGame>();
        NextStep();
    }

    private void NextStep()
    {
        _interact.FindAll<IEnterInNextTurn>();
        
        _turnInteraction = new TurnInteraction();
        GameData<Main>.Turn = _turnInteraction;
    }

    
    public void InstantiateCMSEntity(ViewComponent SetView, Vector3 Position = default, Quaternion Rotation = default)
    {
        if (!SetView.ViewModel)
            throw new NullReferenceException("The Prefab is null");

        SetView.ViewModel = Instantiate(SetView.ViewModel, Position, Rotation);
    }



    public void UpdateGame(float TimeDelta)
    {
        foreach (var Element in InteractionCache<IEnterInUpdate>.AllInteraction)
        {
            Element.Update(TimeDelta);
        }
    }

    public void PhysicUpdateGame(float TimeDelta)
    {
        foreach (var Element in InteractionCache<IEnterInPhysicUpdate>.AllInteraction)
        {
            Element.PhysicUpdate(TimeDelta);
        }
    }

    public void StoppedGame()
    {
        foreach (var Element in InteractionCache<IExitInGame>.AllInteraction)
        {
            Element.Stop();
        }
    } 
    
    private void Awake()
    {
        StartGame();
    }
    private void Update()
    {
        UpdateGame(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        PhysicUpdateGame(Time.deltaTime);
    } 

    private void OnDestroy()
    {
        StoppedGame();
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
