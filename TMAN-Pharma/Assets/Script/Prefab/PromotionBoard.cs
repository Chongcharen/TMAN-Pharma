using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Service.ClassReference;
using System;
using System.Globalization;
using DG.Tweening;

public class PromotionBoard : MonoBehaviour {
    [SerializeField]
    RawImage rawImage;
    public Text head_txt, date_txt;
    Promotion promotion;
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
        DataManager.instance.promotionSelect = promotion;
        EFE_Base.instance.OpenPanelByIndex(Intent.PromotionDetail);
		if (!CacheManager.Instance.WatchPromotion (promotion.promo_id)) {
			CacheManager.Instance.AddWatchPromotion (promotion.promo_id);
		}
    }
    public void SetData(Promotion p)
    {
        promotion = p;
        GenerateBoard();
        head_txt.text = promotion.promo_name;
        //detail_txt.text = promotion.promo_content;
		detail_txt.text = promotion.promo_content;
		detail_txt.OriginText = promotion.promo_content;
        date_txt.text = Utils.ConvertTimestampToDate(promotion.timestamp);

		FadeBoard ();

    }
    void GenerateBoard()
    {
		
		if (ImageManager.instance.IsImage (promotion.image_list [0]) == true) {
			rawImage.texture = ImageManager.instance.GetImage (promotion.image_list [0]);
			rawImage.SetNativeSize ();
			rect = rawImage.GetComponent<RectTransform> ();
			rect.anchorMin = new Vector2(0, 0);
			rect.anchorMax = new Vector2(1, 1);
			rect.offsetMin = Vector2.zero;
			rect.offsetMax = Vector2.zero;
		} else {
			StartCoroutine (StaticCoroutine.LoadImage (promotion.image_list [0], texture => {
				rawImage.texture = texture;
				rawImage.SetNativeSize ();
				rect = rawImage.GetComponent<RectTransform> ();
				rect.anchorMin = new Vector2(0, 0);
				rect.anchorMax = new Vector2(1, 1);
				rect.offsetMin = Vector2.zero;
				rect.offsetMax = Vector2.zero;
				ImageManager.instance.AddImageLink(promotion.image_list [0],texture);
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
			if (CacheManager.Instance.WatchPromotion (promotion.promo_id)) {
				i.DOColor (new Color32 (230, 230, 230, 255),0.5f);

			} else {
				i.DOFade (1, 0.5f);
			}

		}
		foreach (Text t in texts) {
			t.DOFade (1, 0.5f);
		}
		foreach (RawImage r in rawImages) {
			if (CacheManager.Instance.WatchPromotion (promotion.promo_id)) {
				r.DOColor (new Color32(230, 230, 230, 255),0.5f);

			} else {
				r.DOFade (1, 0.5f);
			}
		}
	}
}
