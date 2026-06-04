using UnityEngine;
using UnityEngine.Events;

public class DragAndDrop : MonoBehaviour
{
    public UnityEvent onDrag, dragging, onDrop;
    private Vector3 offSet;
    private Vector3 mousePos;
    void OnMouseDown()
    {
        onDrag?.Invoke();
        Vector3 startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offSet = startMousePos - transform.position;
        
    }
    void OnMouseDrag()
    {
        dragging?.Invoke();
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = currentMousePos - offSet;
        transform.position = mousePos;
    }
    void OnMouseUp()
    {
        onDrop?.Invoke();
    }
    
}
