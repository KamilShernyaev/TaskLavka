using UnityEngine;

public static class Extension
{
    public static bool TryGetInteractableObject(Camera camera, float distance, out RaycastHit hit, LayerMask layerMask)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, distance, layerMask))
        {
            return true;
        }
        return false;
    }
}