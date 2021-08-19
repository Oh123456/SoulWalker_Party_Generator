using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    Image bg;

    public int Job { get; private set; }

    public void Setting(int num)
    {
        bg.color = CharacterData.GetCharacterColor(num);
        text.text = CharacterData.GetCharacterName(num);
        Job = num;
    }

    public void OnClick()
    {
        PlayerAddPanel.Instance.selected.Setting(this.Job);
        //PlayerAddPanel.Instance.selected.gameObject.SetActive(false);
        PlayerAddPanel.Instance.ScrollOff();
        //PlayerAddPanel.Instance.selected.gameObject.SetActive(true);
    }
    
}
