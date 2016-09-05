using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class IntentManager : MonoBehaviour {
    private static IntentManager instance;
    private bool canSlide = true;
    private List<int> historyPage;
    public int currentIntent = 0;
    public int previousIntent = 0;
    public int nextIntent = 0;
    public List<AbstractIntent> listPage;
    public static IntentManager GetInstance() {
        if (instance == null)
        {
            instance = new GameObject("Intentmanager").AddComponent<IntentManager>();
        }
        return instance;
    }
    void Awake()
    {
        instance = this;
        historyPage = new List<int>();
    }
    public void SetIntent(Intent intent)
    {
        if (!canSlide) return;
        nextIntent = (int)intent;
        if (currentIntent == nextIntent) return;
        SetAnimateSlide();
        previousIntent = currentIntent;
        listPage[nextIntent].gameObject.SetActive(true);
        listPage[nextIntent].SetNextPosition();
        listPage[nextIntent].SlideLeft();
        listPage[currentIntent].SlideLeft(false);
        currentIntent = nextIntent;

        historyPage.Add(previousIntent);
    }

    public void Back()
    {
        if (!canSlide) return;
        if (currentIntent == previousIntent) return;
        if (historyPage.Count <= 0) return;
        SetAnimateSlide();
        nextIntent = previousIntent;
        listPage[historyPage[historyPage.Count - 1]].gameObject.SetActive(true);
        listPage[historyPage[historyPage.Count - 1]].SetPreviousPosition();
        listPage[historyPage[historyPage.Count - 1]].SlideRight();
        listPage[currentIntent].SlideRight(false);
        currentIntent = previousIntent;
        historyPage.RemoveAt(historyPage.Count - 1);
    }

    #region TestSlide
    public void Nextpage()
    {
        SetIntent(Intent.Menu);
    }
	
    public void PreviousPage()
    {
        Back();

    }

    #endregion
    void SetAnimateSlide()
    {
        canSlide = false;
        StartCoroutine("DelaySlide");
    }
    IEnumerator DelaySlide()
    {
        yield return new WaitForSeconds(1);
        canSlide = true;
    }
}
public enum Intent{
    Login = 0,Register = 1 , Menu =2
}