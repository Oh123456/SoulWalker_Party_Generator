using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAddPanel : MonoSingleton<PlayerAddPanel>
{
    [SerializeField]
    InputField nameInputField;
    [SerializeField]
    InputField DPSInputField;
    [SerializeField]
    CharacterList Scroll;

    public CharacterSelect selected;

    private void Start()
    {
        Scroll.gameObject.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        selected.Setting(4);
    }

    public void On_Ok()
    {
        string userName = "'" + nameInputField.text + "'";
        DataBaseManager.Instance.InsertInto(new string[] { userName, DPSInputField.text , selected.Job.ToString() ,"1"});
        MainPanel.Instance.PlayerDataScroll.AddData(nameInputField.text, float.Parse(DPSInputField.text) , selected.Job);
        this.gameObject.SetActive(false);

    }

    public void On_Cancel()
    {
        this.gameObject.SetActive(false);
    }


    public void ScrollUp()
    {
        Scroll.gameObject.SetActive(true);
    }

    public void ScrollOff()
    {
        Scroll.gameObject.SetActive(false);
    }
}
