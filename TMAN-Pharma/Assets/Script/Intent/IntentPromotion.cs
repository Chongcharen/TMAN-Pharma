using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
using DG.Tweening;
public class IntentPromotion : AbstractIntent{
    
    public Transform content;
 
    public GameObject boardPrefab, newsPrefab;
	public Text headerText;

    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        ClearBoard();
        if(DataManager.instance.GetMemberType() == 0)
        {
			headerText.text = "โปรโมชั่น";
        }
        else
        {
			headerText.text = "ข่าวสาร";
        }
        content.transform.localPosition = Vector3.zero;
        Events.instance.PageReady_Dispatch();
		StartCoroutine (DelayUpdateIntent ());
    }
	IEnumerator UpdateBoardNews()
    {
        List<MemberNEWS> news = DataManager.instance.memberNews;
        GameObject go;
        foreach (MemberNEWS p in news)
        {
            go = Instantiate(newsPrefab) as GameObject;
			go.GetComponent<NEWSBoard>().SetData(p);
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            
			yield return new WaitForSeconds (0.5f);
        }
    }
	IEnumerator UpdateBoardPromotion()
    {
        List<Promotion> promotion = DataManager.instance.promotion;
        GameObject go;
       foreach(Promotion p in promotion)
        {
            go = Instantiate(boardPrefab) as GameObject;
			go.GetComponent<PromotionBoard>().SetData(p);
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            
			yield return new WaitForSeconds (0.5f);
        }
    }
	void ClearBoard()
    {

		for (var i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
	IEnumerator DelayUpdateIntent(){
		yield return new WaitForSeconds (0.5f);
		if(DataManager.instance.GetMemberType() == 0)
		{
			StartCoroutine(UpdateBoardPromotion());
		}
		else
		{
			StartCoroutine(UpdateBoardNews());
		}
	}
}
