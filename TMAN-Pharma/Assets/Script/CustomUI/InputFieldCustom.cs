using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class InputFieldCustom : MonoBehaviour {
    [SerializeField]
    InputField input;
    [SerializeField]
    Button b_click;

    void Start()
    {
        b_click.onClick.AddListener(CallInputField);
    }
    void CallInputField()
    {
        input.Select();
    }
}
