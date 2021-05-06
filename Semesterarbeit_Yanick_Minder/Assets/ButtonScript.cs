using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ControllerMovement control;
    void Update()
    {
        if (ispressed)
        {
            control.Jumpvoid();
        }
        
        
    }
    bool ispressed = false;
    public void OnPointerDown( PointerEventData eventData )
    {
        ispressed = true;
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        ispressed = false;
    }
}
