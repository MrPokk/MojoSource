using Engin.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseInteraction 
{
    public virtual Priority PriorityInteraction { get => Priority.Medium; }
}

interface IEnterInStart
{
    public void Start();
}

interface IInitInMain
{
    public void Init();
}

interface IEnterInUpdate
{
    void Update(float TimeDelta);
}
