using UnityEngine;

public class FirstPersonPickup : MonoBehaviour, IPicker
{
    [SerializeField] private float _pickUpDistance = 2f;
    [SerializeField] private Transform _handArea;
    [SerializeField] private LayerMask _layerMask;

    private Camera _camera;
    private IElement _helpObject;
    private IElement _interactableObject;

    private void Awake() => 
        _camera = Camera.main;

    private void Update() 
        => HandleHighlighting();

    private void HandleHighlighting()
    {
        if (Extension.TryGetInteractableObject(_camera, _pickUpDistance, out RaycastHit hit, _layerMask))
        {
            if (!hit.collider.TryGetComponent(out _helpObject)) return;

            if (_interactableObject != null)
            {
                _helpObject.HighlightIfSameTypeAndId(_interactableObject);
            }
            else
            {
                _helpObject.HighlightIfSameTypeAndId(null);
                _helpObject = null;
            }
        }
    }

    public void PickUpObject()
    {
        if(Extension.TryGetInteractableObject(_camera, _pickUpDistance, out RaycastHit hit, _layerMask))
        {
            if(_interactableObject != null) return;

            IElement interactableObject = hit.collider.GetComponent<IElement>();
            if(interactableObject == null) return;
            
            _interactableObject = interactableObject;
            _interactableObject.OnPickUp(_handArea);
        }
    }

    public void DropObject()
    { 
        if(Extension.TryGetInteractableObject(_camera, _pickUpDistance, out RaycastHit hit, _layerMask))
        {
            _interactableObject?.Connect(hit.collider.gameObject);
        }
        else
        {
            _interactableObject?.OnDrop();
        }
        _interactableObject = null;
    }
}