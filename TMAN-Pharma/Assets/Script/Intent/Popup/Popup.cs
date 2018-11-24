using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Popup : MonoBehaviour,IPopup {

    public Text description_txt;
    public IAcceptPopup popupCallback;
    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
    public virtual void SetMessage(string message)
    {
        description_txt.text = message;
    }
    public void AddAccept(IAcceptPopup iPopup)
    {
        popupCallback = iPopup;
    }
}
