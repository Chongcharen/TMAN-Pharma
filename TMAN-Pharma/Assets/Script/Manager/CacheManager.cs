using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CacheManager : MonoBehaviour {
	public static string WATCH_PROMOTION = "watch_promotion";
	public static string WATCH_NEWS = "watch_news";
	public static string WATCH_PRODUCT = "watch_product";

	private static CacheManager instance;
	Dictionary<string,bool> promotionWatching;
	Dictionary<string,bool> newsWatching;
	Dictionary<string,bool> productWatching;

	public static CacheManager Instance{
		get{
			if (instance == null) {
				instance = new GameObject ("[@CacheManager]").AddComponent<CacheManager> ();
				instance.LoadCache ();
			}
			return instance;
		}

	}

	public bool WatchPromotion(string promotion_id){
		return promotionWatching.ContainsKey(promotion_id);
	}
	public bool WatchNEWS(string news_id){
		return newsWatching.ContainsKey(news_id);
	}
	public bool WatchProduct(string product_id){
		return productWatching.ContainsKey(product_id
		);
	}

	public void AddWatchPromotion(string promotion_id){
		promotionWatching.Add (promotion_id,true);
		PlayerPrefsUtility.SaveDict (WATCH_PROMOTION, promotionWatching);
	}
	public void AddWatchNEWS(string news_id){
		newsWatching.Add (news_id,true);
		PlayerPrefsUtility.SaveDict (WATCH_NEWS, newsWatching);
	}
	public void AddWatchProduct(string product_id){
		productWatching.Add (product_id,true);
		PlayerPrefsUtility.SaveDict (WATCH_PRODUCT, productWatching);
	}

	public void LoadCache(){
		if (PlayerPrefs.HasKey (WATCH_PRODUCT)) {
			productWatching = PlayerPrefsUtility.LoadDict<string,bool> (WATCH_PRODUCT);
		} else {
			productWatching = new Dictionary<string, bool> ();
		}
		if (PlayerPrefs.HasKey (WATCH_PROMOTION)) {
			promotionWatching = PlayerPrefsUtility.LoadDict<string,bool> (WATCH_PROMOTION);
		} else {
			promotionWatching = new Dictionary<string, bool> ();
		}
		if (PlayerPrefs.HasKey (WATCH_NEWS)) {
			newsWatching = PlayerPrefsUtility.LoadDict<string,bool> (WATCH_NEWS);
		} else {
			newsWatching = new Dictionary<string, bool> ();
		}
	}
}
