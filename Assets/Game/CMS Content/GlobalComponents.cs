using Engin.Utility;
using System;
using System.Linq;
using UnityEngine;

namespace Game.CMS_Content
{
    public class ViewComponent : IComponent
    {
        public void LoadModel<T>(string modelPath)
        {
            var ViewModelGameObjects = Resources.LoadAll<GameObject>(modelPath);

            if (!ViewModelGameObjects.Any())
                Debug.LogError($"Check if there is an object in the path: {modelPath}");

            foreach (var ViewModel in ViewModelGameObjects)
            {
                if (!ViewModel.TryGetComponent<ModelView<T>>(out var view))
                    continue;
                
                Prefab = ViewModel;
                return;
            }
            Debug.LogError($"ViewComponent: {typeof(T).Name} model could not be loaded");
        }

        public GameObject Prefab;
    }

}
