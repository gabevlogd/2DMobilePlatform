using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.touchCount >= 3) return;
        IsPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) => IsPressed = false;

}
