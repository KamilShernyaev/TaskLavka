using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Element : MonoBehaviour, IElement
{
    private readonly Color HighlightColor = Color.green;
    private readonly Color DefaultColor = Color.white;

    [SerializeField] private int _id = 1;
    [SerializeField] private bool _canPickUp = true;
    [SerializeField] private ElementType _elementType;
    [SerializeField] private IElement _parentElement;

    public int ID => _id;
    public bool CanPickUp => _canPickUp;
    public Vector3 StartPosition => _startPosition;
    public ElementType ElementType => _elementType;
    public IElement ParentElement => _parentElement;
    public bool IsAttached {get => _isAttached; set => _isAttached = value;}
    
    private Renderer _renderer;
    private Vector3 _startPosition;
    private bool _isAttached = false;

    private void Awake()
    {
        if(transform.parent != null && transform.parent.TryGetComponent(out IElement component))
        {
            _parentElement = component;
        }

        _startPosition = transform.position;
        _renderer = GetComponentInChildren<Renderer>() ?? throw new Exception("Renderer component not found!");
        SetColor(DefaultColor);
    }

    private void Update() => 
        HighlightIfSameTypeAndId(this);

    public void OnPickUp(Transform handArea)
    {
        if(_canPickUp == false) return;
        
        transform.SetParent(handArea);
        transform.localRotation = handArea.rotation;
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 2f);
    }

    public void OnDrop()
    {
        transform.SetParent(null);
        transform.position =  _startPosition;
    }

    public void Connect(GameObject interactableObject)
    {
        if (!_canPickUp) return;

        IElement element = interactableObject.GetComponent<IElement>();
        Debug.Log("ID: " + element.ID + " " + "Object Name:" + element.ElementType + " + " + "ID: " + _id + " " + "Object Name:" + ElementType);

        if (element == null)
        {
            OnDrop();
            return;
        }

        if (element.ElementType != _elementType)
        {
            OnDrop();
            return;
        }

        if (element.ID != _id)
        {
            OnDrop();
            return;
        }

        if(element.ParentElement != null && !element.ParentElement.IsAttached)
        {
            OnDrop();
            return;
        }

        AttachElement(element);
    }

    public void HighlightIfSameTypeAndId(IElement element)
    {
        if(element == null || CanPickUp || IsAttached) return;
        if(_parentElement != null && !_parentElement.IsAttached) return;

        if((Object)element == this) 
        {
            SetColor(DefaultColor);
        }
        else if (element.ElementType == _elementType && element.ID == _id)         
        {
            SetColor(HighlightColor);
            SetVisibility(true);
        }
    }

    public void SetColor(Color color) => 
        _renderer.material.color = color;

    public void SetVisibility(bool value) =>
         _renderer.enabled = value;

    private void AttachElement(IElement element)
    {
        element.SetVisibility(true);
        element.IsAttached = true;
        element.SetColor(DefaultColor);
        transform.SetParent(null);
        Destroy(gameObject);
    }
}
