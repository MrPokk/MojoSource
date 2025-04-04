using Engin.Utility;
using Game.Script.HUD;
using System;
using System.Linq;
using UnityEngine;

namespace Game.CMS_Content
{
    public class ViewComponent : IComponent
    {
        
        public ModelView ViewModel;

        public void LoadView<T>(string viewPath)
        {
            
            var ViewModelGameObjects = Resources.LoadAll<GameObject>(viewPath);

            if (!ViewModelGameObjects.Any())
                Debug.LogError($"Check if there is an object in the path: {viewPath}");

            foreach (var ViewGameObject in ViewModelGameObjects)
            {
                if (!ViewGameObject.TryGetComponent<T>(out var viewModel))
                    continue;

                ViewModel = viewModel as ModelView;
                return;
            }

            GameObject gameObject = Resources.Load(PathResources.ERORR_VIEW) as GameObject;
            
            if (gameObject != null)
                ViewModel = gameObject.GetComponent<ViewError>();

            Debug.LogError($"ViewComponent: {typeof(T).Name} model could not be loaded");
        }

    }

}
