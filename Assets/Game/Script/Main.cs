using Engin.Utility;
using Game.CMS_Content;
using Game.CMS_Content.Cards;
using Game.CMS_Content.Entity;
using Game.Script.Utility;
using System;
using UnityEngine;
using UnityEngine.Serialization;
public class Main : MonoBehaviour, IMain
{
    private Interaction _interact;
    
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

        GridController = new GridController(new(12, 7), 0.8f, GridView);

        MainCamera = Camera.main;

        GameData<Main>.Corotine  = new GameObject("[Coroutine]").AddComponent<CoroutineUtility>();
        DontDestroyOnLoad(GameData<Main>.Corotine.gameObject);

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
        
        _interact.FindAll<IEnterInUpdate>();
        _interact.FindAll<IExitInGame>();
        _interact.FindAll<IEnterInPhysicUpdate>();
        _interact.FindAll<IExitInGame>();
        
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
