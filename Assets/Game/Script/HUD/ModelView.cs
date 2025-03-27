using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class ModelView<T> : MonoBehaviour 
{
    public virtual Type ID { get => GetType(); }
}

