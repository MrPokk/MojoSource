using Engin.Utility;
using Game.CMS_Content;
using Game.CMS_Content.Entitys;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.Script.ECS.Global_Interactions;
using Game.Script.Utility.FromGame;
using System;
using UnityEngine;
using UnityEngine.Serialization;
public class Main : MonoBehaviour, IMain
{
    private Interaction _interact;
    private TurnInteraction _turnInteraction;

    [SerializeField]
    private GridView _gridPlayer;
    [SerializeField]
    private GridView _gridEnemy;
    [SerializeField]
    private UIRoot _uiRoot;

    [field: SerializeField]
    public HandCards HandCards { get; private set; }

    private GridController _gridController;
  //  private GridController _gridControllerEnemy;

    public Camera MainCamera { get; private set; }
    


    public void StartGame()
    {
        _interact = new Interaction();
        _interact.Init();

        GameData<Main>.Boot = this;


        _gridController = new GridController(new(12, 5), 0.73f, _gridPlayer);
      //  _gridControllerEnemy = new GridController(new(5, 5), 0.73f, _gridEnemy);


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
        _interact.FindAll<IEnterInAttack>();


        _turnInteraction = new TurnInteraction();
        GameData<Main>.Turn = _turnInteraction;
    }


    public void InstantiateCMSEntity(ViewComponent SetView, Vector3 Position = default, Quaternion Rotation = default)
    {
        if (!SetView.ViewModel)
            throw new NullReferenceException("The Prefab is null");

        SetView.ViewModel = Instantiate(SetView.ViewModel, Position, Rotation);
    }

    public GridController GetGridController(CMSEntity cmsEntity)
    {
        if (cmsEntity is IPlayer)
        {
            return _gridController;
        }
        if (cmsEntity is IEnemy)
        {
            return _gridController;
        }
        throw new ArgumentException("ERROR: Grid not found");
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
