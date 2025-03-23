using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class ModelView<T> : MonoBehaviour where T : MonoBehaviour
{
    
    private static T _instance = null;
    public static T Instance {
        get {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            } 
         return _instance;  
        }
    }

}