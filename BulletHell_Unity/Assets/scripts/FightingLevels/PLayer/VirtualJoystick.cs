using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bGImg;
    private Image joystickImg;
    private Vector3 inputVector;

    private void Start()
    {
        bGImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bGImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bGImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bGImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
            inputVector = (inputVector.magnitude > 1.0f)?inputVector.normalized : inputVector;

            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bGImg.rectTransform.sizeDelta.x / 3), inputVector.y * (bGImg.rectTransform.sizeDelta.y / 3), 0);
        }

      
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public Vector3 GetInput()
    {
        return inputVector;
    }
    public float Vertical()
    {
        return inputVector.y;
    }
    public float Horizontal()
    {
        return inputVector.x;
    }

}
