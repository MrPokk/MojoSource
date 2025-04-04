using Engin.Utility;
using Game.CMS_Content;
using Game.CMS_Content.Cards;
using Game.CMS_Content.Entity;
using Game.Script.Utility;
using System;
using UnityEngine;
public class Main : MonoBehaviour, IMain
{
    public static CoroutineUtility _coroutine { get; private set; }
    private Interaction _interact;

    [SerializeField] private GridView gridView;

    public GridController GridController { get; private set; }

    public HandCards HandCards;

    public Camera MainCamera { get; private set; }

    public void StartGame()
    {
        _interact = new Interaction();
        _interact.Init();

        GameData<Main>.Boot = this;

        GridController = new GridController(new(10, 10), 1, gridView);

        MainCamera = Camera.main;

        _coroutine = new GameObject("[Coroutine]").AddComponent<CoroutineUtility>();
        DontDestroyOnLoad(_coroutine.gameObject);

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

    public void InstantiateCMSEntity(ViewComponent SetView, Vector3 Position = default, Quaternion Rotation = default)
    {
        if (!SetView.ViewModel)
            throw new NullReferenceException("The Prefab is null");

        SetView.ViewModel = Instantiate(SetView.ViewModel, Position, Rotation);
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
