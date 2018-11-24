using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChoicePopup : Popup
{
    public Button b_ok,b_cancel;

    void Start()
    {
        b_ok.onClick.AddListener(OnOK);
        b_cancel.onClick.AddListener(OnCancel);
    }
    void OnOK()
    {
        popupCallback.Accept();
    }
    void OnCancel()
    {
        PopupManager.instance.ClosePopup();
    }
}
