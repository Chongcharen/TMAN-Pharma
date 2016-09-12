using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChoicePopup : Popup {
    [SerializeField]
    Button b_ok, b_cancel;
    IPopupOkReference popupRef;
    void Start()
    {
        b_ok.onClick.AddListener(OnOK);
        b_cancel.onClick.AddListener(OnCancel);
    }
    void OnCancel()
    {
        Hide();
    }
    void OnOK() {
        
        if (popupRef != null)
        {
            popupRef.OnOk();
            popupRef = null;
        }
        Hide();
    }
    

    void SetChoiceCallback(string msg, IPopupOkReference popupOk = null)
    {
        description_txt.text = msg;
        popupRef = popupOk;
    }
  
}
public interface IPopupOkReference
{
    void OnOk();
}
