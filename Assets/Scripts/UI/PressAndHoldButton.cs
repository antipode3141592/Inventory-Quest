using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressAndHoldButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} OnPointerClick", this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} OnPointerDown...", this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} OnPointerUp", this);
    }
}
