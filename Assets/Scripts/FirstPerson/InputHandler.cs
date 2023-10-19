using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private  KeyCode runningKey = KeyCode.LeftShift;

    private IMovable _movable;
    private ILookable _lookable;
    private IPicker _pickable;
    
    private void Awake() 
    {
        _movable = GetComponent<IMovable>() ?? throw new Exception("IMovable component not found!");
        _lookable = GetComponent<ILookable>() ?? throw new Exception("ILookable component not found!");
        _pickable = GetComponent<IPicker>() ?? throw new Exception("IPickable component not found!");
    }

    private void Update() 
    {
        ReadMove();
        ReadRun();

        ReadLook();
        ReadPickUp();
    }

    private void ReadMove()
    {
        var directionMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _movable.Move(directionMove);
    }

    private void ReadRun()
    {
        var IsRunning = Input.GetKey(runningKey);
        _movable.Run(IsRunning);
    }

    private void ReadLook()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        _lookable.LookAt(mouseDelta);
    }

    private void ReadPickUp()
    {
        if (Input.GetMouseButton(0))
            _pickable.PickUpObject();
        else if(Input.GetMouseButtonUp(0))
            _pickable.DropObject();
    }
    
}
