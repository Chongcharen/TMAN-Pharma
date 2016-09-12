using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class IntentManager : MonoBehaviour {
    public static IntentManager instance;
    private bool canSlide = true;
    private List<int> historyPage;
    public int currentIndex = 0;
    public int previousIndex = 0;
    public int nextIndex = 0;
    public List<AbstractIntent> listPage;

    private AbstractIntent currentIntent, previousIntent, nextIntent;
    bool intentNextSlide = false;
  
    void Awake()
    {
        instance = this;
        historyPage = new List<int>();
        Events.PageReady += OnPageReadyEvent;
        
    }
    void Start()
    {
       
    }

    

    public void SetIntent(Intent intent)
    {
        Debug.Log("setintent");
        Debug.Log("currentIndex " + currentIndex);
        Debug.Log("nextindex " + nextIndex);
        if (!canSlide) return;
        nextIndex = (int)intent;
        if (currentIndex == nextIndex) return;
        
        intentNextSlide = true;
        previousIndex = currentIndex;
        nextIntent = listPage[nextIndex];
        currentIntent = listPage[currentIndex];
        nextIntent.SetNextPosition();
        nextIntent.gameObject.SetActive(true);
    }

    public void Back()
    {

        if (!canSlide) return;
        if (historyPage.Count <= 0) return;
        if (currentIndex == historyPage[historyPage.Count - 1]) return;
        previousIndex = historyPage[historyPage.Count - 1];
        Events.instance.OpenLoader_Dispatch();
        intentNextSlide = false;
        previousIntent = listPage[previousIndex];
        currentIntent = listPage[currentIndex];
        previousIntent.SetPreviousPosition();
        previousIntent.gameObject.SetActive(true);
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




    #region Event
    private void OnPageReadyEvent()
    {
        Debug.Log("pageready");
        SetAnimateSlide();
        if (intentNextSlide)
        {
            nextIntent.SlideLeft();
            currentIntent.SlideLeft(false);
            currentIndex = nextIndex;
            historyPage.Add(previousIndex);
        }
        else
        {
            if (historyPage.Count <= 0) return;
            previousIntent.SlideRight();
            currentIntent.SlideRight(false);
            currentIndex = previousIndex;
            historyPage.RemoveAt(historyPage.Count - 1); 
        }
    } 
    #endregion
}
public enum Intent{
    Login = 0,Register = 1 ,ForgotPassword = 2, Menu = 3 , Inputaddress = 4
}