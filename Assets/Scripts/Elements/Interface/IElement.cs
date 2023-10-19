using UnityEngine;

public interface IElement : IPickable
{
    int ID {get;}
    Vector3 StartPosition {get;}
    ElementType ElementType {get;}
    bool IsAttached {get; set;}
    IElement ParentElement {get;}
    
    void Connect(GameObject interactableObject);
    void SetColor(Color color);
    void SetVisibility(bool value);
    void HighlightIfSameTypeAndId(IElement pickedElement);
}
