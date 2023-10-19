using UnityEngine;

public class FirstPersonLook : MonoBehaviour, ILookable
{
    [SerializeField] private Transform character;
    [SerializeField] private float sensitivity = 2;
    [SerializeField] private float smoothing = 1.5f;

    private Vector2 velocity;
    private Vector2 frameVelocity;

    void Start() => 
        Cursor.lockState = CursorLockMode.Locked;

    public void LookAt(Vector2 mouseDelta)
    {
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        character.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}