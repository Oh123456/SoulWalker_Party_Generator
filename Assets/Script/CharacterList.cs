using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    [SerializeField]
    CharacterSelect characterSelect;
    [SerializeField]
    RectTransform content;

    private void Awake()
    {
        content.sizeDelta = new Vector2(0.0f , characterSelect.GetComponent<RectTransform>().rect.height * 9);
        for (int i = 0; i < 9; i++)
        {
            Instantiate(characterSelect, content.transform).Setting(i + 1);
        }
    }
}
