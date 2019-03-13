using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool fire;

   void Start()
    {
        fire = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.position.x > gameObject.GetComponent<RectTransform>().position.x && eventData.position.y > gameObject.GetComponent<RectTransform>().position.y)
        {
            fire = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        fire = false;
    }

    public bool GetInput()
    {
        return fire;
    }
}
