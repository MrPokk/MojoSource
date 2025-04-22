using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Script.Utility.FromEditor
{
    public abstract class ScriptableGlobalSetting<T> : ScriptableObject, IDebugUser where T : ScriptableGlobalSetting<T>
    {
        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null)
                {
                    T[] assetSingletons = Resources.LoadAll<T>(PathResources.SETTINGS);
                    if (assetSingletons == null || assetSingletons.Length < 1)
                    {
                        throw new Exception("ERROR: НЕ УДАЛОСЬ НАЙТИ В РЕСУРСАХ ЭКЗЕМПЛЯР НАСТРОЕК");
                    }
                    else if (assetSingletons.Length > 1)
                    {
                        Debug.LogError("ERROR: ТАКОЙ ОБЪЕКТ НАСТРОЕК УЖЕ ЕСТЬ");
                    }
                    _instance = assetSingletons.First();
                }
                return _instance;
            }
        }

        public void OnEnable()
        {
            ScriptableGlobalService.DebugUsers.Add(this);
            DebugUtility.DebugGizmos += DebugOnGizmos;
        }

        public virtual void DebugOnGizmos()
        { }
    }

    public static class ScriptableGlobalService
    {
        public static HashSet<IDebugUser> DebugUsers = new HashSet<IDebugUser>();
    }
    public interface IDebugUser
    {
        public void DebugOnGizmos();
    }
}
