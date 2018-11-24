using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Service.ClassReference;
using System;
using System.Globalization;
using DG.Tweening;
public class NEWSBoard : MonoBehaviour
{
    [SerializeField]
    RawImage rawImage;
    public Text head_txt, date_txt;
    MemberNEWS news;
    [SerializeField]
    Button b_detail;
	RectTransform rect;
	public Text_Extend detail_txt;

    void Start()
    {
        b_detail.onClick.AddListener(OpenDetail);
    }
    void OpenDetail()
    {
        DataManager.instance.newsSelect = news;
		EFE_Base.instance.OpenPanelByIndex (Intent.PromotionDetail);
		if (!CacheManager.Instance.WatchNEWS (news.news_id)) {
			CacheManager.Instance.AddWatchNEWS (news.news_id);
		}
    }
    public void SetData(MemberNEWS p)
    {
        news = p;
        GenerateBoard();
        head_txt.text = news.news_name;
        detail_txt.text = news.news_content;
		detail_txt.OriginText = news.news_content;
        date_txt.text = Utils.ConvertTimestampToDate(news.timestamp);
		FadeBoard ();
    }
    void GenerateBoard()
    {
		if (ImageManager.instance.IsImage (news.image_list[0]) == true) {
			rawImage.texture = ImageManager.instance.GetImage (news.image_list[0]);

			rawImage.SetNativeSize ();
			rect = rawImage.GetComponent<RectTransform> ();
			rect.anchorMin = new Vector2(0, 0);
			rect.anchorMax = new Vector2(1, 1);
			rect.offsetMin = Vector2.zero;
			rect.offsetMax = Vector2.zero;
		} else {
			StartCoroutine (StaticCoroutine.LoadImage (news.image_list[0], texture => {
				rawImage.texture = texture;
				rawImage.SetNativeSize ();
				rect = rawImage.GetComponent<RectTransform> ();
				rect.anchorMin = new Vector2(0, 0);
				rect.anchorMax = new Vector2(1, 1);
				rect.offsetMin = Vector2.zero;
				rect.offsetMax = Vector2.zero;
				ImageManager.instance.AddImageLink(news.image_list[0],texture);
			}));
		}
    }
	void OnEnable(){
		
	}

	void FadeBoard(){
		Image[] images = GetComponentsInChildren<Image>();
		Text[] texts = GetComponentsInChildren<Text>();
		RawImage[] rawImages = GetComponentsInChildren<RawImage>();
		foreach (Image i in images) {
			if (CacheManager.Instance.WatchNEWS (news.news_id)) {
				i.DOColor (new Color32 (230, 230, 230, 255),0.5f);

			} else {
				i.DOFade (1, 0.5f);
			}

		}
		foreach (Text t in texts) {
			t.DOFade (1, 0.5f);
		}
		foreach (RawImage r in rawImages) {
			if (CacheManager.Instance.WatchNEWS (news.news_id)) {
				r.DOColor (new Color32(230, 230, 230, 255),0.5f);

			} else {
				r.DOFade (1, 0.5f);
			}
		}
	}
}
