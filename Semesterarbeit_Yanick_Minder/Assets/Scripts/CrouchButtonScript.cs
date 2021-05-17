using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CrouchButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ControllerMovement control;
    
    public void OnPointerDown( PointerEventData eventData )
    {
        control.crouch = true;
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        control.crouch = false;
    }
}
