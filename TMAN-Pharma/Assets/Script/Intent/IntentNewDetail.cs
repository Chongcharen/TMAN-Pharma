using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Service.ClassReference;
public class IntentNewDetail : AbstractIntent {
	public Transform content,toggleContent;
    
    public GridLayoutGroup gridLayout;
    
    public RectTransform viewportTransform;
    
    public GameObject indicatorPrefab,toggleIndicatorPrefab;
    
	public Text head_txt, date_txt,header_page_txt;
	public Text_Extend detail_txt;
    public override void UpdatePage()
    {
        ClearData();
        Vector2 view = new Vector2(viewportTransform.rect.width, viewportTransform.rect.height);
	
        gridLayout.cellSize = view;
		if(DataManager.instance.GetMember().member_id == 1)
		{
			header_page_txt.text = "โปรโมชั่น";
		}
		else
		{
			header_page_txt.text = "ข่าวสาร";
		}
        base.UpdatePage();
		StartCoroutine (DelayUpdateIntent ());
    }

	void OnDisable(){
		head_txt.text = "";
		detail_txt.text = "";
		date_txt.text = "";
	}
    void UpdateNEWSIndicator()
    {
        MemberNEWS p = DataManager.instance.newsSelect;
        GameObject go;
        for (int i = 0; i < p.image_list.Count; i++)
        {
            go = Instantiate(indicatorPrefab) as GameObject;
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            go.GetComponent<ImageIndicator>().SetImage(p.image_list[i]);

            go = Instantiate(toggleIndicatorPrefab) as GameObject;
            go.transform.SetParent(toggleContent);
            go.transform.localScale = Vector3.one;
        }

        head_txt.text = p.news_name;
        detail_txt.text = p.news_content;
		detail_txt.OriginText = p.news_content;
        date_txt.text = Utils.ConvertTimestampToDate(p.timestamp);
    }
    void UpdatePromotionIndicator()
    {
        Promotion p = DataManager.instance.promotionSelect;
        GameObject go;
        for(int i = 0; i < p.image_list.Count; i++)
        {
            go = Instantiate(indicatorPrefab) as GameObject;
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            go.GetComponent<ImageIndicator>().SetImage(p.image_list[i]);

            go = Instantiate(toggleIndicatorPrefab) as GameObject;
            go.transform.SetParent(toggleContent);
            go.transform.localScale = Vector3.one;
        }

        head_txt.text = p.promo_name;
        detail_txt.text = p.promo_content;
		detail_txt.OriginText = p.promo_content;
        date_txt.text = Utils.ConvertTimestampToDate(p.timestamp);
    }

    void ClearData()
    {
		for(int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
            Destroy(toggleContent.GetChild(i).gameObject);
        }
    }
	IEnumerator DelayUpdateIntent(){
		yield return new WaitForSeconds (0.1f);
		PopupManager.instance.OpenLoading ();
		yield return new WaitForSeconds (1);
		if(DataManager.instance.GetMemberType() == 0)
		{
			UpdatePromotionIndicator();
		}
		else
		{
			UpdateNEWSIndicator();
		}
		PopupManager.instance.ClosePopup ();
	}

}
