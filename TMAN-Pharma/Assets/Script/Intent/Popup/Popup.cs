using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Popup : MonoBehaviour {

    public Text description_txt;

    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
    public void SetMessage(string message)
    {
        description_txt.text = message;
    }
}
