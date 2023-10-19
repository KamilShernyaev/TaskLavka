using UnityEngine;

public interface IPickable
{
    bool CanPickUp {get;}

    void OnPickUp(Transform hand);
    void OnDrop();
}