using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntentForgotPassword : AbstractIntent {

    [SerializeField]
    Button b_getPassword;
    [SerializeField]
    InputField input_email;
    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_getPassword.onClick.AddListener(OnGetPassword);
    }
    void OnGetPassword()
    {
        if (string.IsNullOrEmpty(input_email.text))
        {
            PopupManager.instance.OpenAlert("กรุณากรอกอีเมล์");
            return;
        }
        ServiceRequest.instance.ForgorPasswordRequest(input_email.text);
    }
}
