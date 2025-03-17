using Engin.Utility;
using UnityEngine;
public class Main : MonoBehaviour, IMain
{
    private Interaction Interact = new Interaction();
    public void StartGame()
    {
        Interact.Init();

        GameData<Main>.Boot = this;

        var Ready = Interact.FindAll<IEnterInReady>();
        var Start = Interact.FindAll<IEnterInStart>();

        foreach (var Element in Start)
        {
            Element.Start();
        }

        foreach (var Element in Ready)
        {
            Element.Start();
        }

        GameData<Main>.IsStartGame = true;
    }

    public void UpdateGame(float TimeDelta)
    {
        var Update = Interact.FindAll<IEnterInUpdate>();
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
    private void Start()
    {
        StartGame();
    }
}
