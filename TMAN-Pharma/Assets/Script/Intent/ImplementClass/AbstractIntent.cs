using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AbstractIntent : MonoBehaviour,IPageBehavior {
    Rect screenRect;
    void OnEnable() {
        SetScreenSide();
        ShowPage();
    }
    void SetScreenSide()
    {
       // screenRect = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
    }
    public void ShowPage() {
        Debug.Log("Page is Show!");
    }
    public void LoadData() { }
    public void UpdatePage() { }
    public void SetPosition() { }
    public void SlideLeft() {
        Debug.Log("SlideLeft "+VariableManager.GetInstance().screenCanvas.width);
        transform.DOLocalMoveX(-VariableManager.GetInstance().screenCanvas.width, 0.5f);
    }
    public void SlideRight() {
        transform.DOLocalMoveX(VariableManager.GetInstance().screenCanvas.width, 0.5f);
    }

    
}
