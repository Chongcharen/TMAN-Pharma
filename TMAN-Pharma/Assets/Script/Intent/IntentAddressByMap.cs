using UnityEngine;
using System.Collections;
using DG.Tweening;
public class IntentAddressByMap : AbstractIntent {
    // Use this for initialization
    [SerializeField]
    GameObject map;
    public override void UpdatePage()
    {
        GetComponent<RectTransform>().rect.Set(0, 0, 0, 0);
        transform.localPosition = Vector2.zero;
        //map.SetActive(true);
        MapManager.instance.OpenSavePlace();
        Events.instance.PageReady_Dispatch();
    }
    void OnDisable()
    {
        MapManager.instance.CloseMap();
    }

}
