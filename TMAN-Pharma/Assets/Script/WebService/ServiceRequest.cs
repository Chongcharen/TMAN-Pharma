using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;
using Service.ClassReference;

public class ServiceRequest : MonoBehaviour {

    // Use this for initialization
    public static ServiceRequest instance;
    private object success;
    private object description;
    private object json;
    private string urlRequest = "http://codebee.co.th/project/tman/service/";
    void Awake() {
        instance = this;
        LitJson.JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
        LitJson.JsonMapper.RegisterImporter<int, long>(input => Convert.ToInt64(input));
        LitJson.JsonMapper.RegisterImporter<string, decimal>(input => Convert.ToDecimal(input));
        LitJson.JsonMapper.RegisterImporter<string, int>(input => Convert.ToInt32(input));
        LitJson.JsonMapper.RegisterImporter<string, float>(input => Convert.ToSingle(input));
        LitJson.JsonMapper.RegisterImporter<string, bool>(input => Convert.ToBoolean(input));
        LitJson.JsonMapper.RegisterImporter<string, double>(input => Convert.ToDouble(input));
    }
    void Start() {

    }
    string GetUrlRequest(string methodName)
    {
        return urlRequest + methodName;
    }
    #region Request
	public void LoginRequest(string username, string password,bool startProgram)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
		form.AddField("device_id",SystemInfo.deviceUniqueIdentifier);
        WWW www = new WWW("http://codebee.co.th/project/tman/service/login", form);
        StartCoroutine(ServerCallBack<RawMember>(www, data =>
        {
				if(data.success){
					DataManager.instance.SetMember(data);
					if(!DataManager.instance.isRequestCheckin){
						if(data.member_type == 0)
						{
							GetPromotion(data.member_type,data.group_id);
						}
						else{
							GetNEWSRequest(data.store_id);
						}
						GetMenuRequest(data.member_type);
					}else{
						ServiceRequest.instance.GetAllPosition();
					}
					DataManager.instance.SetCache(username, password);
				}else{
					if(startProgram){
						EFE_Base.instance.OpenPanelByIndex(Intent.Login);
					}else{
						PopupManager.instance.OpenAlert(data.description);
					}

				}
        }));
    }
    public void LogoutRequest(int memberid){
        WWWForm form = new WWWForm();
        form.AddField("memberid", memberid);
        WWW www = new WWW(GetUrlRequest("logout"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            //AcceptCallback acceptCallback = new AcceptCallback();
            //acceptCallback.action = delegate { EFE_Base.instance.OpenPanelByIndex(Intent.Login); };
            //PopupManager.instance.OpenAlert(resp.description, acceptCallback);
        }));
    }
    public void RegisterRequest(string username,string password,string sername,string email,string member_tel1,string member_tel2,
                                string member_tel3,string member_fax,string member_address,int province_id,int store_id,
								string store_name,string code_store,string code_offset)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("sername", sername);
        form.AddField("email", email);
        form.AddField("member_tel1", member_tel1);
        form.AddField("member_tel2", member_tel2);
        form.AddField("member_tel3", member_tel3);
        form.AddField("member_fax", member_fax);
        form.AddField("member_address", member_address);
        form.AddField("province_id", province_id);
        form.AddField("store_id", store_id);
        form.AddField("store_name", store_name);
		form.AddField("code_store", code_store);
        form.AddField("code_offset", code_offset);
        WWW www = new WWW(GetUrlRequest("register"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
				AcceptCallback acceptCallback = new AcceptCallback();
				acceptCallback.action = delegate { EFE_Base.instance.OpenPanelByIndex(Intent.Login); };
				PopupManager.instance.OpenAlert(resp.description,acceptCallback);
        }));
    }
    public void ForgorPasswordRequest(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        WWW www = new WWW(GetUrlRequest("forgotPassword"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            Debug.Log(resp.description);
        }));
    }
    public void UpdateMemberProfile(int member_id, string sername, string member_tel1, string member_tel2, string member_tel3,
                                    string email, string member_fax, int province_id, string member_address, int store_name = -1)
    {
        string methodName = "do_updatememberSale";
        WWWForm form = new WWWForm();
        if (store_name >= 0) {
            form.AddField("store_name", store_name);
            methodName = "do_updatememberCustomer";
        }
        form.AddField("member_id", member_id); form.AddField("sername", sername); form.AddField("member_tel1", member_tel1);
        form.AddField("member_tel2", member_tel2); form.AddField("member_tel3", member_tel3); form.AddField("email", email);
        form.AddField("member_fax", member_fax); form.AddField("province_id", province_id); form.AddField("member_address", member_address);
        WWW www = new WWW(GetUrlRequest(methodName), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            PopupManager.instance.OpenAlert("บันทึกข้อมูลเรียบร้อยแล้ว");
        }));
    }
    public void SavePlaceRequest(int place_id, double pos_latitude, double pos_longitude, byte[] image_path, string filename)
    {
        WWWForm form = new WWWForm();
        form.AddField("place_id", place_id);
		form.AddField("pos_latitude", System.Math.Round(pos_latitude,6).ToString());
		form.AddField("pos_longitude", System.Math.Round(pos_longitude,6).ToString());
        form.AddBinaryData("image_path", image_path, filename);
        WWW www = new WWW(GetUrlRequest("getPosition_Save"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            AcceptCallback acceptCallback = new AcceptCallback();
            acceptCallback.action = delegate { EFE_Base.instance.OpenPanelByIndex(Intent.Menu); };
            PopupManager.instance.OpenAlert("บันทึกสถานที่เรียบร้อย",acceptCallback);
				DataManager.instance.branchSelect = null;
			//GetAllPosition();
        }));
    }
	public void SendRecommendRequest(int member_id,string com_id,string order_commend, byte[] image_path, string filename)
    {
        WWWForm form = new WWWForm();
        form.AddField("member_id", member_id);
        form.AddField("com_id", com_id);
        form.AddField("order_commend", order_commend);
        form.AddBinaryData("image_path", image_path, filename);

        WWW www = new WWW(GetUrlRequest("Recommend"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            PopupManager.instance.OpenAlert("ส่งข้อมูลเรียบร้อยแล้ว");
        }));
    }
    public void CheckinSaveRequest(int member_id , int pos_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("member_id", member_id);
        form.AddField("pos_id", pos_id);
		WWW www = new WWW(GetUrlRequest("CheckIn_save"), form);
        StartCoroutine(ServerCallBack<PlaceCheckin>(www, resp =>
        {
            DataManager.instance.placeCheckin = resp;

				if(resp.success){
					Events.instance.CheckinComplete_Dispatch(resp);
				}else{
					PopupManager.instance.OpenAlert(resp.description);
				}
            
        }));
    }
    public void SendSatisSurvey(int member_id, string title_id, string sur_answer)
    {
        WWWForm form = new WWWForm();
        form.AddField("member_id", member_id);
        form.AddField("title_id", title_id);
        form.AddField("sur_answer", sur_answer);
        WWW www = new WWW(GetUrlRequest("Survey"), form);
		StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
				PopupManager.instance.OpenAlert(resp.description);
        }));
    }
    public void UpdatePointRequest(int member_id, string exp_code)
    {
        WWWForm form = new WWWForm();
        form.AddField("member_id", member_id);
        form.AddField("exp_code", exp_code);
        WWW www = new WWW(GetUrlRequest("updatePoint"), form);
		StartCoroutine(ServerCallBack<ReceiveMemberPoint>(www, receivemember =>
        {
				DataManager.instance.memberProfile.member_point = receivemember.member_point.ToString();
				PopupManager.instance.OpenAlert(receivemember.description);
				Events.instance.UpdateMemberPoint_Dispatch(DataManager.instance.memberProfile.member_point);
        }));
    }


    #region GET REQUEST
    public void GetMemberProfile(int member_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("member_id", member_id);

        WWW www = new WWW(GetUrlRequest("editmemberBYID"), form);
      
        StartCoroutine(ServerCallBackToList<MemberProfile>(www, memberProfile =>
        {
            DataManager.instance.memberProfile = memberProfile[0];
			Events.instance.LoadMemberProfile_Dispatch(DataManager.instance.memberProfile);
				PopupManager.instance.ClosePopup();
        }));
    }
    public void GetCodeAward()
    {
        WWW www = new WWW(GetUrlRequest("getCodeAward"));
        StartCoroutine(ServerCallBackToList<LuckyDrawCode>(www, luckycodes =>
        {
            DataManager.instance.luckyCode = luckycodes;
			Events.instance.LoadLuckyCode_Dispatch();
			PopupManager.instance.ClosePopup();
           // EFE_Base.instance.OpenPanelByIndex(Intent.LuckyDraw);
        }));
    }
public void GetReceiveAward(int member_id, string receive_name, string code_store, string receive_tel
                               , string receive_content, byte[] image_path, string imagename)
    {
        WWWForm form = new WWWForm();
        form.AddField("member_id", member_id);
        form.AddField("receive_name", receive_name);
        form.AddField("code_store", code_store);
        form.AddField("receive_tel", receive_tel);
        form.AddField("receive_content", receive_content);
        form.AddBinaryData("image_path", image_path, imagename);
        WWW www = new WWW(GetUrlRequest("getReceiveAward"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            PopupManager.instance.OpenAlert("ส่งข้อมูลเรียบร้อยแล้ว");
        }));
    }
    public void GetTitleSurvey()
    {
        WWW www = new WWW(GetUrlRequest("titleSurvey"));

		/*string aaa = "{\"title\":[{\"title_id\":\"1\",\"title_name\":\"\\u0e2a\\u0e34\\u0e19\\u0e04\\u0e49\\u0e32\",\"title_status\":\"1\",\"timestamp\":\"2016-09-12 15:02:47\"},{\"title_id\":\"2\",\"title_name\":\"\\u0e01\\u0e32\\u0e23\\u0e43\\u0e2b\\u0e49\\u0e02\\u0e49\\u0e2d\\u0e21\\u0e39\\u0e25\\u0e2a\\u0e34\\u0e19\\u0e04\\u0e49\\u0e32\",\"title_status\":\"1\",\"timestamp\":\"2016-09-12 15:02:47\"}]," +
			"\"title_content\":\"[url=http:\\/\\/www.codebee.co.th][\\/url][b][u]\\u0e42\\u0e2d\\u0e40\\u0e1b\\u0e23\\u0e48\\u0e32\\u0e2e\\u0e31\\u0e25\\u0e42\\u0e25\\u0e27\\u0e35\\u0e19\\u0e2d\\u0e48\\u0e27\\u0e21[\\/u][\\/b][\\/url][url=http:\\/\\/www.tmanpharma.co.th\\/[\\/url][b][u]\\u0e42\\u0e2d\\u0e40\\u0e1b\\u0e23\\u0e48\\u0e32\\u0e2e\\u0e31\\u0e25\\u0e42\\u0e25\\u0e27\\u0e35\\u0e19\\u0e2d\\u0e48\\u0e27\\u0e21[\\/u][\\/b][\\/url]\"}";
		SatisFactionData wrapper = JsonHelper.FromSingleJson<SatisFactionData>(aaa);
		Debug.Log ("wrapper " + wrapper);
		Debug.Log ("count " + wrapper.title_content);
		DataManager.instance.satisFactionData = wrapper;
		//wrapper.title_content = "As early as 2009 [link_player=1971]<color=#FBAF9F>[u]Elon Musk[/u]</color>[/link_player] indicated a desire to make the [link_item=9]<color=#5D72FF>[u]Falcon 9[/u]</color>[/link_item] the first fully reusable launch vehicle.";
		Events.instance.LoadSatis_Dispatch();*/
		StartCoroutine(ServerCallBack<SatisFactionData>(www, titleData =>
        {
			DataManager.instance.satisFactionData = titleData;
			Events.instance.LoadSatis_Dispatch();
            //EFE_Base.instance.OpenPanelByIndex(Intent.SatisFaction);
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetURLProduct()
    {
        WWW www = new WWW(GetUrlRequest("getUrlProduct"));
        StartCoroutine(ServerCallBackToList<ProductLink>(www, link =>
        {
            Application.OpenURL(link[0].url_product);
            PopupManager.instance.ClosePopup();
        }));
    }
	public void GetMenuRequest(int member_type){
		WWWForm form = new WWWForm();
		form.AddField("member_type", member_type);
		WWW www = new WWW(GetUrlRequest("settingmenu"), form);
		StartCoroutine(ServerCallBackToList<MenuService>(www, menu =>
			{
				DataManager.instance.menuService = menu;
                EFE_Base.instance.OpenPanelByIndex(Intent.Menu);
                EFE_Base.instance.ClearHistory();

            }));
	}
	public void GetRewardPointRequest(int place_id,int member_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("place_id", place_id);
		form.AddField("member_id", member_id);

        WWW www = new WWW(GetUrlRequest("RewardPoint"), form);
        StartCoroutine(ServerCallBack<RewardPoint>(www, reward =>
        {
            Events.instance.LoadRewardCode_Dispatch(reward.exp_code);
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetStoreNEWSRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("store_id", 1);
        WWW www = new WWW(GetUrlRequest("getNewsBYStoreID"), form);
        StartCoroutine(ServerCallBackToList<MemberNEWS>(www, resp =>
        {
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetNEWSRequest(int store_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("member_type",1);
        form.AddField("store_id", store_id);
        WWW www = new WWW(GetUrlRequest("getNewORPromotion"), form);
        StartCoroutine(ServerCallBackToList<MemberNEWS>(www, news =>
        {
            DataManager.instance.memberNews = news;
            PopupManager.instance.ClosePopup();
        }));
    }
	public void GetPromotion(int member_type, string group_id)
    {
        WWWForm form = new WWWForm();
		form.AddField("member_type",member_type);
		form.AddField ("group_id", group_id);
        WWW www = new WWW(GetUrlRequest("getNewORPromotion"), form);
        StartCoroutine(ServerCallBackToList<Promotion>(www, promotion =>
        {
            DataManager.instance.promotion = promotion;
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetPromotionBYID(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("promo_id", id);
        WWW www = new WWW(GetUrlRequest("getPromotionBYID"), form);
        StartCoroutine(ServerCallBackToList<Promotion>(www, resp =>
        {
           
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetPlaceRequest(string code_store,string group_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("code_store", code_store);
        form.AddField("group_id", group_id);
        WWW www = new WWW(GetUrlRequest("getPlaceBYCode"), form);
        StartCoroutine(ServerCallBackToList<Place>(www, resp =>
        {
			
				if(resp.Count > 1){
					DataManager.instance.branchPlace = resp;
					EFE_Base.instance.OpenPanelByIndex(Intent.FindShopResult);
				}else{
					if (resp[0] != null)
					{
						 Events.instance.LoadPlace_Dispatch((Place)resp[0]);
					}
				}
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetCheckinByID(string checkin_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("checkin_id", checkin_id);
        WWW www = new WWW(GetUrlRequest("getCheckInByID"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            Debug.Log(resp.description);
        }));
    }
    public void GetAllProvince()
    {
        WWW www = new WWW(GetUrlRequest("getAllProvince"));
        StartCoroutine(ServerCallBackToList<Province>(www, provinceData =>
        {
            DataManager.instance.province = provinceData;
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetAllStore()
    {
        WWW www = new WWW(GetUrlRequest("getAllstore"));
        StartCoroutine(ServerCallBackToList<Store>(www, storeData =>
        {
            DataManager.instance.store = storeData;
            PopupManager.instance.ClosePopup();

        }));
    }
    public void GetAllGroupSale()
    {
        WWW www = new WWW(GetUrlRequest("getAllGroupSale"));
        StartCoroutine(ServerCallBackToList<GroupSale>(www, groupSaleData =>
        {
            DataManager.instance.groupSale = groupSaleData;
            PopupManager.instance.ClosePopup();
        }));
    }
    public void GetAllPosition()
    {
        Debug.Log("Get all position");
        WWW www = new WWW(GetUrlRequest("getALLPosition"));
        StartCoroutine(ServerCallBackToList<LatLongPosition>(www, latlongData =>
        {
            DataManager.instance.latlongList = latlongData;
            foreach(LatLongPosition l in DataManager.instance.latlongList){
            }
				try{
					Events.instance.LoadLocation_Dispatch();
				}catch(Exception e){
				}
        }));
    }
	public void GetAllHeaderPDF(){
		WWW www = new WWW(GetUrlRequest("getAllHeaderpdf"));
		StartCoroutine(ServerCallBackToList<HeaderFile>(www, headerList =>
			{
				DataManager.instance.headerPDFList = headerList;
				PopupManager.instance.ClosePopup();
				Events.instance.LoadHeaderFile_Dispatch(headerList);
               // EFE_Base.instance.OpenPanelByIndex(Intent.PDFProduct);
			}));
	}
    public void GetAllRecommend()
    {
        WWW www = new WWW(GetUrlRequest("getAllRecommend"));

        StartCoroutine(ServerCallBackToList<RecommendTitle>(www, recommendList =>
        {
            DataManager.instance.recommends = recommendList;
            PopupManager.instance.ClosePopup();
			Events.instance.LoadRecommend_Dispatch(recommendList);
            //EFE_Base.instance.OpenPanelByIndex(Intent.Recommend);
        }));
    }
    
    //Search
    public void SearchPlaceRequest(int province_id,string keyword)
    {
        WWWForm form = new WWWForm();
        form.AddField("province_id", province_id);
        form.AddField("keyword", keyword);
        WWW www = new WWW(GetUrlRequest("SearchPlace"),form);
        StartCoroutine(ServerCallBackToList<PlaceFilter>(www, placeFilterData =>
        {
            DataManager.instance.placeFilter = placeFilterData;
            foreach (var data in DataManager.instance.placeFilter)
            {
                Debug.Log("Data " + data.place_id);
            }
            EFE_Base.instance.OpenPanelByIndex(Intent.FindPlaceResult);
        }));
    }
	public void SearchPlaceByID(int place_id)
    {
        Debug.Log("SearchPlaceByID " + place_id);
        WWWForm form = new WWWForm();
		form.AddField("place_id", place_id);
		WWW www = new WWW(GetUrlRequest("SearchBYID"),form);
		StartCoroutine(ServerCallBackToList<PlaceFilter>(www, placeFilterData =>
        {
				Events.instance.SearchPlace_Dispatch(placeFilterData[0]);
        }));
    }
	public void SearchPlaceByID(string place_id)
	{
		WWWForm form = new WWWForm();
		form.AddField("place_id", place_id);
		WWW www = new WWW(GetUrlRequest("SearchBYID"),form);
		//string a = "[{\"place_id\":\"106\",\"place_name\":\"\\u0e23\\u0e49\\u0e32\\u0e19\\u0e40\\u0e2d\\u0e1f\\u0e41\\u0e2d\\u0e19\\u0e14\\u0e4c\\u0e1e\\u0e35\\u0e40\\u0e2e\\u0e47\\u0e25\\u0e41\\u0e04\\u0e23\\u0e4c\",\"place_address\":\"33\\/60-61\\u00a0\\u0e16.\\u0e1e\\u0e38\\u0e17\\u0e18\\u0e21\\u0e13\\u0e11\\u0e25\\u0e2a\\u0e32\\u0e22\\u00a04  \\u0e2d.\\u0e01\\u0e23\\u0e30\\u0e17\\u0e38\\u0e48\\u0e21\\u0e41\\u0e1a\\u0e19 \\u0e08.\\u0e2a\\u0e21\\u0e38\\u0e17\\u0e23\\u0e2a\\u0e32\\u0e04\\u0e23\",\"place_tel\":\"02\\u00a0813-0065\",\"pos_latitude\":\"13.717124\",\"pos_longitude\":\"100.330124\"},{\"place_id\":\"106\",\"place_name\":\"\\u0e23\\u0e49\\u0e32\\u0e19\\u0e40\\u0e2d\\u0e1f\\u0e41\\u0e2d\\u0e19\\u0e14\\u0e4c\\u0e1e\\u0e35\\u0e40\\u0e2e\\u0e47\\u0e25\\u0e41\\u0e04\\u0e23\\u0e4c\",\"place_address\":\"33\\/60-61\\u00a0\\u0e16.\\u0e1e\\u0e38\\u0e17\\u0e18\\u0e21\\u0e13\\u0e11\\u0e25\\u0e2a\\u0e32\\u0e22\\u00a04  \\u0e2d.\\u0e01\\u0e23\\u0e30\\u0e17\\u0e38\\u0e48\\u0e21\\u0e41\\u0e1a\\u0e19 \\u0e08.\\u0e2a\\u0e21\\u0e38\\u0e17\\u0e23\\u0e2a\\u0e32\\u0e04\\u0e23\",\"place_tel\":\"02\\u00a0813-0065\",\"pos_latitude\":\"13.717124\",\"pos_longitude\":\"100.330124\"}]";
		//	List<PlaceFilter> wrapperList = JsonHelper.FromJsonList<PlaceFilter>(a);
		//Debug.Log("aaaa "+wrapperList.Count);
		StartCoroutine(ServerCallBackToList<PlaceFilter>(www, placeFilterData =>
			{
				Events.instance.SearchAreaPlace_Dispatch(placeFilterData);
			}));
	}
    #endregion

    #endregion


    IEnumerator ServerCallBack<T>(WWW www, Action<T> callBack)
    {
        PopupManager.instance.OpenLoading();
        yield return StartCoroutine(new WWWRequest(www));
        /*if (www.isDone) {
            Debug.Log(www.text);
            ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(www.text);
			//if (!typeof(T).IsAssignableFrom(response.GetType()))
			if(response == null || !response.success)
            {
                T wrapper = JsonHelper.FromSingleJson<T>(www.text);
                callBack(wrapper);
            }
            else
            {
                Debug.Log(response.description);
                PopupManager.instance.OpenAlert(response.description);
            }

        }*/
		if (www.isDone)
		{
			//Debug.Log (www.text);
			try
			{
				T wrapper = JsonHelper.FromSingleJson<T>(www.text);
				callBack(wrapper);
			} catch (Exception e)
			{
				ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(www.text);
				Debug.Log("โหลดข้อมูลไม่ได้ " + e.Message);
				PopupManager.instance.OpenAlert(response.description);
			}
		}

    }
    IEnumerator ServerCallBackToList<T>(WWW www, Action<List<T>> callBack)
    {
        Debug.Log("Load link " + www.url);
        PopupManager.instance.OpenLoading();
        yield return StartCoroutine(new WWWRequest(www));
        if (www.isDone)
        {
           // Debug.Log(www.text);
            try
            {
                List<T> wrapperList = JsonHelper.FromJsonList<T>(www.text);
                callBack(wrapperList);
            } catch (Exception e)
            {
                ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(www.text);
                Debug.Log("โหลดข้อมูลไม่ได้ " + e.Message);
                PopupManager.instance.OpenAlert(response.description);
            }

        }
    }
   
    
    #region Request แบบเก่า

    IEnumerator Request_Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", "test");
        form.AddField("password", "12345678");
        form.AddField("sername", "aaaa");
        form.AddField("email", "aaa@aaa.aaa");
        form.AddField("member_tel1", "123456");
        form.AddField("member_tel2", "7890123");
        form.AddField("member_tel3", "00000");
        form.AddField("member_fax", "023232323");
        form.AddField("member_address", "thailand");
        form.AddField("province_id", "samutprakarn");
        form.AddField("store_id", "fsdf");
        form.AddField("store_name", "asdasd");
        form.AddField("code_store", "fsdfdsf");
        form.AddField("code_offset", "dsfsdfdsf");
        WWW www = new WWW("http://codebee.co.th/project/tman/service/register", form);
        yield return StartCoroutine(new WWWRequest(www));
        if (www.isDone)
        {
            ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(www.text);
            Debug.Log(response.description);
        }
	
    }
    IEnumerator Request_Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        WWW www = new WWW("http://codebee.co.th/project/tman/service/login", form);
        yield return StartCoroutine(new WWWRequest(www));
        if (www.isDone)
        {
            Debug.Log(www.text);
            ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(www.text);
            Debug.Log(response.success);
        }
    } 
    #endregion
}

