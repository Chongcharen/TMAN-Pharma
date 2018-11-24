using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
public class DataManager : MonoBehaviour {

    public static DataManager instance;
    public MemberProfile memberProfile;
    private RawMember member;
    private Place placeData;
    private List<MemberNEWS> _memberNews;
    private List<Promotion> _promotions;
    private List<GroupSale> _groupSale;
    private List<Province> _province;
    private List<Store> _store;
    private List<LatLongPosition> _latlongList;
    private List<PlaceFilter> _placeFilter;
	private List<PlaceFilter> _placeFilterArea;
	private List<MenuService> _menuService;
	private List<HeaderFile> _headerPDFList;
	private SatisFactionData _satisFactionData;
    private List<RecommendTitle> _recommends;
    private List<LuckyDrawCode> _luckyCode;
	private List<Place> _branchPlace;
    private Vector2 _currentLatLong;
    private PlaceCheckin _placeCheckin;
    private PlaceFilter _placeSelect;
    public MemberNEWS newsSelect;
    public Promotion promotionSelect;
	public Place branchSelect;

    public Vector2 userLocation;

    public string username;
    public string password;
    public Vector2 currentPosition;
    public Vector2 targetPosition;
    public byte[] image_byteArray;
    public byte[] image_recommend_byteArray;
    public string fileImage;
    public string fileRecommendImage;
	public bool isFindDirection = false;
	public bool isInputAddress = false;
	public bool isRequestCheckin = false;
    void Awake()
    {
        instance = this;
		branchSelect = null;
    }


    #region MyRegion
    public void SetMember(RawMember _member)
    {
        member = _member;
    }
    public void SetCache(string _username , string _password)
    {
        username = _username;
        password = _password;
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
    }
    public void GetCache()
    {
		string username_cache = PlayerPrefs.GetString("username");
		string password_cache = PlayerPrefs.GetString("password");
        if (!string.IsNullOrEmpty(username_cache) && !string.IsNullOrEmpty(password_cache))
        {
			ServiceRequest.instance.LoginRequest(username_cache, password_cache,true);
        }
        else
        {
            EFE_Base.instance.OpenPanelByIndex(Intent.Login);
            
        }
    }
	public int GetMemberType()
    {
        return member.member_type;
    }
    public RawMember GetMember()
    {
        return member;
    }
    #endregion
    

    //Place
    public void SetPlace(Place place)
    {
        placeData = place;
    }
    public Place getPlace()
    {
        return placeData;
    }
    
    //groupsale
    public List<GroupSale> groupSale
    {
        get { return _groupSale; }
        set { _groupSale = value; }
    }
    public List<Province> province
    {
        get { return _province; }
        set { _province = value; }
    }
    public List<Store> store
    {
        get { return _store; }
        set { _store = value; }
    }
    public List<LatLongPosition> latlongList
    {
        get { return _latlongList; }
        set {
            Debug.Log("Set latlonglist " + value);
            foreach(var data in value)
            {
                Debug.Log("data " + data.pos_longitude + "," + data.pos_longitude);
            }
            _latlongList = value; 
        }
    }
    public PlaceCheckin placeCheckin
    {
        get { return _placeCheckin; }
        set { _placeCheckin = value; }
    }


    public List<PlaceFilter> placeFilter
    {
        get { return _placeFilter; }
        set { _placeFilter = value; }
    }
	public List<PlaceFilter> placeFilterArea
	{
		get { return _placeFilterArea; }
		set { _placeFilterArea = value; }
	}

    public PlaceFilter placeSelect
    {
        get { return _placeSelect; }
        set { _placeSelect = value; }
    }
    public List<Promotion> promotion
    {
        get { return _promotions; }
        set { _promotions = value; }
    }
    public List<MemberNEWS> memberNews
    {
        get { return _memberNews; }
        set { _memberNews = value; }
    }
	public List<MenuService> menuService
	{
		get { return _menuService; }
		set { _menuService = value; }
	}
	public List<HeaderFile> headerPDFList
	{
		get { return _headerPDFList; }
		set { _headerPDFList = value; }
	}
	public SatisFactionData satisFactionData
    {
		get { return _satisFactionData; }
		set { _satisFactionData = value; }
    }

    public List<RecommendTitle> recommends
    {
        get { return _recommends; }
        set { _recommends = value; }
    }
    public List<LuckyDrawCode> luckyCode
    {
        get { return _luckyCode; }
        set { _luckyCode = value; }
    }
	public List<Place> branchPlace
	{
		get { return _branchPlace; }
		set { _branchPlace = value; }
	}

    void Start()
    {
        ServiceRequest.instance.GetAllProvince();
        ServiceRequest.instance.GetAllStore();
        ServiceRequest.instance.GetAllGroupSale();
        ServiceRequest.instance.GetAllPosition();
    }
    //Event
    void OnChangePosition()
    {
       _currentLatLong = OnlineMapsLocationService.instance.position;
    }

    public void Logout()
    {
        member = null;
        SetCache("", "");
        //service/logout   //member_id
        ServiceRequest.instance.LogoutRequest(DataManager.instance.memberProfile.member_id);
        EFE_Base.instance.ClearHistory();
		DataManager.instance.isRequestCheckin = false;
        EFE_Base.instance.OpenPanelByIndex(Intent.Login);
    }

}
