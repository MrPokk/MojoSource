using JetBrains.Annotations;
using UnityEngine;

namespace Game.Script.GlobalComponent.Interface
{
    public sealed class Raycasting
    {
        public static bool TryGetRaycastObject<T>(Vector3 mousePosition, out T monoBehaviour) where T : MonoBehaviour
        {
            var hitObject = Physics2D.Raycast(mousePosition, Vector2.zero).collider;

            if (hitObject)
            {
                var insideComponent = hitObject.gameObject.TryGetComponent<T>(out T component);
                var raycastComponentTry = hitObject.gameObject.TryGetComponent<RaycastObject>(out var raycastComponent);

                if (!raycastComponentTry)
                {
                    Debug.Log("ERROR: NOT COMPONENT RAYCAST");

                    monoBehaviour = null;
                    return false;
                }

                if (insideComponent)
                {
                    monoBehaviour = component;
                    return true;
                }

            }

            monoBehaviour = null;
            return false;
        }


        public static bool TryGetRaycastObject(Vector3 mousePosition, out GameObject monoBehaviour)
        {
            var hitObject = Physics2D.Raycast(mousePosition, Vector2.zero).collider;

            if (hitObject)
            {
                var raycastComponentTry = hitObject.gameObject.TryGetComponent<RaycastObject>(out var raycastComponent);

                if (raycastComponentTry)
                    monoBehaviour = raycastComponent.gameObject;

            }
            Debug.Log("ERROR: NOT COMPONENT RAYCAST");

            monoBehaviour = null;
            return false;
        }
    }


}
