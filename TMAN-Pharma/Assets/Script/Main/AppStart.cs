using UnityEngine;
using System.Collections;

public class AppStart : MonoBehaviour {
    void Start()
    {
        VariableManager.GetInstance().screenCanvas = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
        IntentManager.GetInstance().SetIntent(Intent.Login);
    }
}
