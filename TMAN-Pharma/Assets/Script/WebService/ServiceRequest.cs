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
    void Awake(){
		instance = this;
        LitJson.JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
        LitJson.JsonMapper.RegisterImporter<int, long>(input => Convert.ToInt64(input));
        LitJson.JsonMapper.RegisterImporter<string, decimal>(input => Convert.ToDecimal(input));
        LitJson.JsonMapper.RegisterImporter<string, int>(input => Convert.ToInt32(input));
        LitJson.JsonMapper.RegisterImporter<string, float>(input => Convert.ToSingle(input));
        LitJson.JsonMapper.RegisterImporter<string, bool>(input => Convert.ToBoolean(input));
    }
	void Start(){
		
	}
    string GetUrlRequest(string methodName)
    {
        return urlRequest + methodName;
    }
    #region Request
    public void LoginRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        WWW www = new WWW("http://codebee.co.th/project/tman/service/login", form);
        StartCoroutine(ServerCallBack<RawMember>(www, data =>
        {
            DataManager.instance.SetMember(data);
           // IntentManager.instance.SetIntent(Intent.Menu);
			EFE_Base.instance.OpenPanelByIndex(Intent.Menu);

        }));
    }
    public void RegisterRequest()
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
        WWW www = new WWW(GetUrlRequest("register"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            Debug.Log(resp.description);
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
    public void UpdateMemberProfile(string member_id,string sername,string member_tel1,string member_tel2,string member_tel3 ,
                                    string email , string member_fax ,string province_id,string member_address,string store_name = "")
    {
        string methodName = "do_updatememberSale";
        WWWForm form = new WWWForm();
        if (!string.IsNullOrEmpty(store_name)) {
            form.AddField("store_name", store_name);
            methodName = "do_updatememberCustomer";
        }
        form.AddField("member_id", member_id); form.AddField("sername", sername); form.AddField("member_tel1", member_tel1);
        form.AddField("member_tel2", member_tel2); form.AddField("member_tel3", member_tel3); form.AddField("email", email);
        form.AddField("member_fax", member_fax); form.AddField("province_id", province_id); form.AddField("member_address", member_address);
        WWW www = new WWW(GetUrlRequest(methodName), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            Debug.Log(resp.success);
        }));
    }

    public void GetStoreNEWSRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("store_id", 1);
        WWW www = new WWW(GetUrlRequest("getNewsBYStoreID"), form);
        StartCoroutine(ServerCallBackToList<MemberNEWS>(www, resp =>
        {
            Debug.Log(resp[0].image_list[0]); //ใช้ได้
        }));
    }
    public void GetNEWSRequest(string news_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("news_id", news_id);
        WWW www = new WWW(GetUrlRequest("getNewsBYID"), form);
        StartCoroutine(ServerCallBackToList<MemberNEWS>(www, resp =>
        {
            Debug.Log(resp[0].image_list[0]); //ใช้ได้
        }));
    }
    public void GetAllPromotionRequest()
    {
        WWW www = new WWW(GetUrlRequest("getAllPromotion"));
        StartCoroutine(ServerCallBackToList<Promotion>(www, resp =>
        {
            Debug.Log(resp[0].promo_name); //ใช้ได้
        }));
    }
    public void GetPromotionBYID(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("promo_id", id);
        WWW www = new WWW(GetUrlRequest("getPromotionBYID"), form);
        StartCoroutine(ServerCallBackToList<Promotion>(www, resp =>
        {
            Debug.Log(resp[0].promo_name); //ใช้ได้
        }));
    }

    //place_id,pos_latitude,pos_longitude,image_path
    public void GetPlaceRequest(string code_store)
    {
        WWWForm form = new WWWForm();
        form.AddField("code_store",code_store);
        WWW www = new WWW(GetUrlRequest("getPlaceBYCode"), form);
        StartCoroutine(ServerCallBackToList<Place>(www, resp =>
        {
            if (resp[0] != null)
            {
                Events.instance.LoadPlace_Dispatch((Place)resp[0]);
            }
        }));
    }

    /*
     {place_id,pos_latitude,pos_longitude,image_path}*/
    public void SavePlaceRequest(int place_id,double pos_latitude,double pos_longitude,byte[] image_path,string filename)
    {
        WWWForm form = new WWWForm();
        form.AddField("place_id", place_id);
        form.AddField("pos_latitude", pos_latitude.ToString());
        form.AddField("pos_longitude", pos_longitude.ToString());
        form.AddBinaryData("image_path", image_path, filename);
        WWW www = new WWW(GetUrlRequest("getPosition_Save"), form);
        StartCoroutine(ServerCallBack<ServiceResponse>(www, resp =>
        {
            Debug.Log(resp.success);
        }));
    }

    public void GetAllProvince()
    {
        WWW www = new WWW(GetUrlRequest("getAllProvince"));
        StartCoroutine(ServerCallBackToList<Province>(www, resp =>
        {
            Debug.Log(resp[0].PROVINCE_NAME); //ใช้ได้
        }));
    }

    public void GetAllStore()
    {
        WWW www = new WWW(GetUrlRequest("getAllstore"));
        StartCoroutine(ServerCallBackToList<Store>(www, resp =>
        {
            Debug.Log(resp[0].store_name); //ใช้ได้
        }));
    }

    #endregion


    IEnumerator ServerCallBack<T>(WWW www ,Action<T> callBack)
    {
        yield return StartCoroutine(new WWWRequest(www));
        if (www.isDone){
            Debug.Log(www.text);
            ServiceResponse response = JsonHelper.FromSingleJson<ServiceResponse>(www.text);
            if (response.success)
            {
                T wrapper = JsonHelper.FromSingleJson<T>(www.text);
                callBack(wrapper);
            }
            else
            {
                Debug.Log(response.description);
            }
            
        }
       
    }
    IEnumerator ServerCallBackToList<T>(WWW www, Action<List<T>> callBack)
    {
        yield return StartCoroutine(new WWWRequest(www));
        if (www.isDone)
        {
            Debug.Log(www.text);
            try
            {
                List<T> wrapperList = JsonHelper.FromJsonList<T>(www.text);
                callBack(wrapperList);
            }catch(Exception e)
            {
                Debug.Log("โหลดข้อมูลไม่ได้ " + e.Message);
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

