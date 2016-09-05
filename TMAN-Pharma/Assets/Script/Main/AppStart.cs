using UnityEngine;
using System.Collections;

public class AppStart : MonoBehaviour {
    void Awake()
    {
        VariableManager.GetInstance().screenCanvas = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
    }
    void Start()
    {
        IntentManager.GetInstance().SetIntent(Intent.Login);
        ServiceRequest.instance.LoginRequest("aa", "aa");
    }
}
