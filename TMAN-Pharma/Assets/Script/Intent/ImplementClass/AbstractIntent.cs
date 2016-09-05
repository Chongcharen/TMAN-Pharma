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
    
    }
    public void LoadData() { }
    public void UpdatePage() { }
    public void SetPreviousPosition(){
        transform.localPosition = new Vector3(-VariableManager.GetInstance().screenCanvas.width, 0, 0);
    }
    public void SetNextPosition(){
        transform.localPosition = new Vector3(VariableManager.GetInstance().screenCanvas.width, 0, 0);
    }
    public void SlideLeft(bool SetActiveWhenComplete = true) {
        transform.DOLocalMoveX(GetComponent<RectTransform>().localPosition.x - VariableManager.GetInstance().screenCanvas.width, 0.7f).SetDelay(0.2f).OnComplete(() => SlideComplete(SetActiveWhenComplete)) ;
    }
    public void SlideRight(bool SetActiveWhenComplete = true) {
        transform.DOLocalMoveX(GetComponent<RectTransform>().localPosition.x + VariableManager.GetInstance().screenCanvas.width , 0.7f).SetDelay(0.2f).OnComplete(() => SlideComplete(SetActiveWhenComplete));
    }

    void SlideComplete(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }



}
