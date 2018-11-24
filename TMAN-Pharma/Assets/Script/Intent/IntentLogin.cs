using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IntentLogin : AbstractIntent {
    public Button b_login, b_register, b_forgotpassword;

    public InputField username_txt, password_txt;
   
    public override void AddButtonListeners()
    {
        //base.AddButtonListeners();
        b_login.onClick.AddListener(OnLogin);
        b_register.onClick.AddListener(Register);
        b_forgotpassword.onClick.AddListener(ForgotPassword);
    }
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        EFE_Base.instance.ClearHistory();
        Events.instance.PageReady_Dispatch();
    }
    void OnLogin()
    {
        if (string.IsNullOrEmpty(username_txt.text))
        {
            PopupManager.instance.OpenAlert("กรุณากรอก username");
            return;
        }
        if (string.IsNullOrEmpty(password_txt.text))
        {
            PopupManager.instance.OpenAlert("กรุณากรอก password");
            return;
        }
		ServiceRequest.instance.LoginRequest(username_txt.text,password_txt.text,false);
    }
    void Register()
    {
        EFE_Base.instance.OpenPanelByIndex(Intent.Register);
    }
    void ForgotPassword()
    {
        EFE_Base.instance.OpenPanelByIndex(Intent.ForgotPassword);
    }

    
}
