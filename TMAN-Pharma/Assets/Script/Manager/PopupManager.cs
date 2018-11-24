using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class PopupManager : MonoBehaviour {
    public static PopupManager instance;
    public GameObject[] popupList;

    private GameObject target;
    void Awake()
    {
        instance = this;
    }
    public void OpenAlert(string msg, IAcceptPopup iPopup = null, POPUP popupMode = POPUP.ALERT)
    {
        target = popupList[(int)popupMode];
        EFE_Base.instance.OpenOverlayPanel(target);
        target.GetComponent<Popup>().SetMessage(msg);
        if(iPopup == null)
        {
            AcceptCallback callBack = new AcceptCallback();
            callBack.action = delegate { EFE_Base.instance.CloseCurrentOverlayPanel(); };
			target.GetComponent<Popup> ().AddAccept (callBack);
        }
        else
        {
            target.GetComponent<Popup>().AddAccept(iPopup);
        }
    }
    public void OpenLoading()
    {
        EFE_Base.instance.OpenOverlayPanel(popupList[(int)POPUP.LOAD]);
    }
    public void ClosePopup()
    {
        EFE_Base.instance.CloseCurrentOverlayPanel();
    }
}
