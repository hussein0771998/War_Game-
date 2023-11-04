using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ManagerContRot : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private Image imgCharRotArea, imgCharRotStick;
    private Vector2 posOut;
   // public Gun gun;
    void Start()
    {
        imgCharRotArea = GetComponent<Image>();
        imgCharRotStick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgCharRotArea.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out posOut))
        {
            posOut.x = posOut.x / (imgCharRotArea.rectTransform.sizeDelta.x);
            posOut.y = posOut.y / (imgCharRotArea.rectTransform.sizeDelta.y);
            if (posOut.magnitude > 1.0f)
            {
                posOut = posOut.normalized;
            }
            imgCharRotStick.rectTransform.anchoredPosition = new Vector2(
                posOut.x * (imgCharRotStick.rectTransform.sizeDelta.x / 7),
                posOut.y * (imgCharRotStick.rectTransform.sizeDelta.y / 7));


        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        imgCharRotStick.rectTransform.anchoredPosition = Vector2.zero;
        // gun.Shoot();
        PlayerGun.ins.Shoot();
    }
    public float InputRotVertical()
    {
        if (posOut.y != 0)
            return posOut.y;
        else
            return 0;
    }
    public float InputRotHorizontal()
    {
        if (posOut.x != 0)
            return posOut.x;
        else
            return 0;
    }
}
