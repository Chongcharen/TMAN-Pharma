using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Service.ClassReference;
using DG.Tweening;
public class ProductBoard : MonoBehaviour {

    [SerializeField]
    RawImage rawImage;
    public Text head_txt, date_txt;
    HeaderFile headerFile;
    [SerializeField]
    Button b_detail;
	public Text_Extend detail_txt;

    void Start()
    {
        b_detail.onClick.AddListener(OpenDetail);
    }
    void OpenDetail()
    {
        Application.OpenURL(headerFile.file_link);
		if (!CacheManager.Instance.WatchProduct (headerFile.file_id.ToString ())) {
			CacheManager.Instance.AddWatchProduct (headerFile.file_id.ToString ());
		}
    }
    public void SetData(HeaderFile p)
    {
        headerFile = p;
        GenerateBoard();
        head_txt.text = headerFile.file_name;
        detail_txt.text = headerFile.file_content;
		detail_txt.OriginText = headerFile.file_content;
		date_txt.text = Utils.ConvertTimestampToDate (p.timestamp);
		Invoke ("FadeBoard", 0.1f);
    }
    void GenerateBoard()
    {
		if (ImageManager.instance.IsImage (headerFile.image_pic) == true) {
			rawImage.texture = ImageManager.instance.GetImage (headerFile.image_pic);
			rawImage.SetNativeSize ();
		} else {
			StartCoroutine (StaticCoroutine.LoadImage (headerFile.image_pic, texture => {
				rawImage.texture = texture;
				rawImage.SetNativeSize ();
				ImageManager.instance.AddImageLink(headerFile.image_pic,texture);
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
			if (CacheManager.Instance.WatchProduct (headerFile.file_id.ToString ())) {
				i.DOColor (new Color32 (240, 240, 240, 255),0.5f);

			} else {
				i.DOFade (1, 0.5f);
			}

		}
		foreach (Text t in texts) {
			t.DOFade (1, 0.5f);
		}
		foreach (RawImage r in rawImages) {
			if (CacheManager.Instance.WatchProduct (headerFile.file_id.ToString ())) {
				r.DOColor (new Color32(240, 240, 240, 255),0.5f);

			} else {
				r.DOFade (1, 0.5f);
			}
		}
	}
}
