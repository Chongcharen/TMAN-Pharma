using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IntentLogin : AbstractIntent {
    [SerializeField]
    Button b_login, b_register, b_forgotpassword;
    [SerializeField]
    InputField username_txt, password_txt;
    void Start()
    {
        b_login.onClick.AddListener(Login);
        b_register.onClick.AddListener(Register);
        b_forgotpassword.onClick.AddListener(ForgotPassword);
    }
    void Login()
    {
       ServiceRequest.instance.LoginRequest(username_txt.text,password_txt.text);
    }
    void Register()
    {
        IntentManager.instance.SetIntent(Intent.Register);
    }
    void ForgotPassword()
    {
        IntentManager.instance.SetIntent(Intent.ForgotPassword);
    }

    
}
