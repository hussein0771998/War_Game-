using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManagerJoystic : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image imgJoysticBg;
    public Image imgJoystic;
    private Vector2 posInput;
    public bool walk;
    

    // Start is called before the first frame update
    void Start()
    {
        imgJoysticBg = GetComponent<Image>();
        walk = false;
    }

    // Update is called once per frame
   
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgJoysticBg.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out posInput))
        {
            posInput.x = posInput.x / (imgJoysticBg.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (imgJoysticBg.rectTransform.sizeDelta.y);
           // Debug.Log(posInput.x.ToString() + "//" + posInput.y.ToString());

            // normalize
            if (posInput.magnitude > 1.0f)
            {
                posInput = posInput.normalized;
            }

            //Joystic move
            imgJoystic.rectTransform.anchoredPosition = new Vector2(
                posInput.x * (imgJoysticBg.rectTransform.sizeDelta.x / 2),
                 posInput.y * (imgJoysticBg.rectTransform.sizeDelta.y) / 2);
         
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        walk = true;
        imgJoystic.color = Color.blue;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        imgJoystic.rectTransform.anchoredPosition = Vector2.zero;
        walk = false;
        imgJoystic.color = Color.LerpUnclamped(Color.blue,Color.white,2f);
    }

    public float inputHorizantal()
    {
        if (posInput.x != 0)
        {
            return posInput.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float inputVertical()
    {
        if (posInput.y != 0)
        {
            return posInput.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }


}
