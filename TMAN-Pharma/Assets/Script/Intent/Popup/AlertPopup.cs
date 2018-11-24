using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class AlertPopup : Popup {
    [SerializeField]
    Button b_ok;
   
    void Start()
    {
        b_ok.onClick.AddListener(OnOK);
    }
    void OnOK()
    {
        popupCallback.Accept();
    }


}
