using UnityEngine;
using System.Collections;

public class IntentFindDirection : AbstractIntent {

    [SerializeField]
    GameObject map;
    void Start()
    {
        AddButtonListeners();

    }

    public override void UpdatePage()
    {
        GetComponent<RectTransform>().rect.Set(0, 0, 0, 0);
        transform.localPosition = Vector2.zero;
        Events.instance.PageReady_Dispatch();
    }
    void OnDisable()
    {
    }
    public override void OnBack()
    {
        base.OnBack();
    }
}
