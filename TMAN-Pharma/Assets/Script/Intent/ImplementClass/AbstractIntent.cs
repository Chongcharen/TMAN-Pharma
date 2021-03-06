﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class AbstractIntent : MonoBehaviour,IPageBehavior {
    [SerializeField]
    Button b_back;
    Rect screenRect;
    void Start()
    {
        AddButtonListeners();
    }
    void OnEnable() {
        SetScreenSide();
        ShowPage();
        UpdatePage();
    }
    void SetScreenSide()
    {
       // screenRect = GameObject.Find("Canvas").GetComponent<RectTransform>().rect;
    }
    public virtual void ShowPage() {
    
    }
    public virtual void AddButtonListeners()
    {
        b_back.onClick.AddListener(OnBack);
    }
    public void ShowBackButton() { }
    public void HideBackButton() { }
    public void LoadData() { }
    public virtual void UpdatePage() {
		if (!VariableManager.GetInstance.canDispatchListener)
			return;
        Events.instance.PageReady_Dispatch();
    }

    public void SetPreviousPosition(){
        transform.localPosition = new Vector3(-VariableManager.GetInstance.screenCanvas.width, 0, 0);
    }
    public void SetNextPosition(){
        transform.localPosition = new Vector3(VariableManager.GetInstance.screenCanvas.width, 0, 0);
    }
    public void SlideLeft(bool SetActiveWhenComplete = true) {
        transform.DOLocalMoveX(GetComponent<RectTransform>().localPosition.x - VariableManager.GetInstance.screenCanvas.width, 0).SetDelay(0).OnComplete(() => SlideComplete(SetActiveWhenComplete)) ;

    }
    public void SlideRight(bool SetActiveWhenComplete = true) {
        transform.DOLocalMoveX(GetComponent<RectTransform>().localPosition.x + VariableManager.GetInstance.screenCanvas.width , 0).SetDelay(0).OnComplete(() => SlideComplete(SetActiveWhenComplete));
    }

    void SlideComplete(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
    public virtual void OnBack()
    {
        EFE_Base.instance.OpenPreviousIntent();
    }




}
