using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldFocus : MonoBehaviour , IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<InputField>().ActivateInputField();
    }
}
