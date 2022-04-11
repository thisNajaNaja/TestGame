using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class PressButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private bool _isPressed = false;
    public UnityEvent _press;
    public UnityEvent _letOff;
    
    public bool IsPressed => _isPressed;


    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        _press?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        _letOff?.Invoke();
    }

    
}
