using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float runSpeed = 10;
    
    private CharacterController _characterController;
    private Vector2 targetVelocity;
    private float targetMovingSpeed;

    void Awake() => 
        _characterController = GetComponent<CharacterController>();

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(targetVelocity.x, 0, targetVelocity.y);
        _characterController.SimpleMove(transform.rotation * direction * targetMovingSpeed);
    }

    public void Move(Vector3 directionMove) => 
        targetVelocity = directionMove;

    public void Run(bool IsRunning) => 
        targetMovingSpeed = IsRunning ? runSpeed : speed;
}