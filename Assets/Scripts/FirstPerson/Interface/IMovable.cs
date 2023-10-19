using UnityEngine;

public interface IMovable
{
    void Move(Vector3 directionMove);
    void Run(bool IsRunning);
}
