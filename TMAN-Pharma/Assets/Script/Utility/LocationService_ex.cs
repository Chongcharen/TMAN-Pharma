using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
public class LocationService_ex : MonoBehaviour {

	public static LocationService_ex instance;
	public GameObject map;
	LatLongPosition latPos;
	LocationService locationService;
	double currentX;
	double currentY;
	int distance;
	void Awake(){
		instance = this;
		locationService = new LocationService ();
        //StartCoroutine("UpdateMyGPS");
	}
	void OnEnable(){
		Events.OnSearchPlaceComplete += Events_OnSearchPlaceComplete;
		Events.OnSearchAreaPlaceComplete += Events_OnSearchAreaPlaceComplete;
		Events.CheckinComplete += Events_CheckinComplete;
	}
	void OnDisable(){
		Events.OnSearchAreaPlaceComplete -= Events_OnSearchAreaPlaceComplete;
		Events.OnSearchPlaceComplete -= Events_OnSearchPlaceComplete;
		Events.CheckinComplete -= Events_CheckinComplete;
	}



	public void OpenCheckin(){
		DataManager.instance.isRequestCheckin = true;
		Events.OnLoadLocation += Events_OnLoadLocation;

		string username_cache = PlayerPrefs.GetString("username");
		string password_cache = PlayerPrefs.GetString("password");
		ServiceRequest.instance.LoginRequest (username_cache,password_cache,false);
	}

	public bool isEnableByUser(){
		#if !UNITY_EDITOR
			if (!locationService.isEnabledByUser) {
				PopupManager.instance.OpenAlert ("กรุณาเปิดใช้งานการระบุตำแหน่ง");
			}
			return locationService.isEnabledByUser;
		#else
			return true;
		#endif
	}

    IEnumerator UpdateMyGPS(){
        yield return new WaitForSeconds(30f);
        StartCoroutine("UpdateMyGPS");
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        // Start service before querying location
        Input.location.Start(0.1f, 0.1f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            PopupManager.instance.OpenAlert("กรุณาตรวจสอบตำแหน่งGPS");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            PopupManager.instance.OpenAlert("กรุณาตรวจสอบตำแหน่งGPS");
            yield break;
        }
        else
        {
            DataManager.instance.userLocation = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            PopupManager.instance.OpenAlert("Lat : "+DataManager.instance.userLocation.x+" long : "+DataManager.instance.userLocation.y);

            //// Access granted and location value could be retrieved
            //CheckinNow();
            //print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }


	IEnumerator CheckinByTime(){
		yield return new WaitForSeconds (0.5f);
        if (!Input.location.isEnabledByUser)
        {
            PopupManager.instance.OpenAlert("กรุณาตรวจสอบตำแหน่งGPS");
            yield break;
        }

		// Start service before querying location
		Input.location.Start(0.1f,0.1f);

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			PopupManager.instance.OpenAlert("กรุณาตรวจสอบตำแหน่งGPS");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			PopupManager.instance.OpenAlert("กรุณาตรวจสอบตำแหน่งGPS");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			CheckinNow();
			print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
		}

		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

	void OnLocationChanged(Vector2 pos){
		currentX = System.Math.Round (pos.y, 6);
		currentY = System.Math.Round (pos.x, 6);
		CheckinNow ();
		OnlineMapsLocationService.instance.restoreAfter = 60;
		OnlineMapsLocationService.instance.updateDistance = 10;
		OnlineMapsLocationService.instance.OnLocationChanged -= OnLocationChanged;
		map.SetActive (false);
	}

	void CheckinNow(){
		
		//ServiceRequest.instance
		//currentX = System.Math.Round(Input.location.lastData.latitude,6);
		//currentY = System.Math.Round(Input.location.lastData.longitude,6);
		latPos = Utils.FindnearCheckin (currentX,currentY);
		string place_id_list = "";
		List<string> areaList = Utils.FindnearArea(currentX,currentY,1000);

		if (areaList.Count <= 0) {
			PopupManager.instance.OpenAlert ("ไม่มีสถานที่ใกล้เคียงที่สามารถ checkin ได้");
			return;
		}
		foreach (string l in areaList) {
			place_id_list = place_id_list+l+",";
		}
		place_id_list = place_id_list.Remove (place_id_list.Length -1 , 1);
		ServiceRequest.instance.SearchPlaceByID (place_id_list);
		//DataManager.instance.placeFilterArea = Utils.FindnearArea (currentX, currentY);
		//EFE_Base.instance.OpenPanelByIndex (Intent.PlaceAreaCheckin);

//		Debug.Log ("latPos.place_id " + latPos.place_id);
//		Debug.Log ("latpos.posID"+latPos.pos_id);
	//	ServiceRequest.instance.SearchPlaceByID (latPos.place_id);
		//Input.location.Stop();
	}

	void Events_OnLoadLocation(){
		//Input.location.Start(0.01f,0.01f);
		StartCoroutine (CallLocation());
		//StartCoroutine (CheckinByTime ());
		/*Events.OnLoadLocation -= Events_OnLoadLocation;
		map.SetActive (true);
		OnlineMapsLocationService.instance.OnLocationChanged += OnLocationChanged;*/
	}
	IEnumerator CallLocation(){
		PopupManager.instance.OpenLoading ();
		map.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		OnlineMapsLocationService.instance.restoreAfter = 1;
		OnlineMapsLocationService.instance.updateDistance = 1;
		OnlineMapsLocationService.instance.StartLocationService (1, 1);
		OnlineMapsLocationService.instance.position = Vector2.zero;
		OnlineMapsLocationService.instance.OnLocationChanged += OnLocationChanged;
		Events.OnLoadLocation -= Events_OnLoadLocation;
		//CheckinNow ();
	}
	void Events_OnSearchPlaceComplete (PlaceFilter place)
	{
		//Debug.Log ("Events_OnSearchPlaceComplete " + place.place_id);
		//Debug.Log ("Placename  " + place.place_name);
		StartCoroutine (SearchLocation (place));
	}
	void Events_OnSearchAreaPlaceComplete (List<PlaceFilter> places)
	{
		
		DataManager.instance.placeFilterArea = places;

		EFE_Base.instance.OpenPanelByIndex (Intent.PlaceAreaCheckin);
	}
	IEnumerator SearchLocation(PlaceFilter place){
		//Debug.Log ("SearchLocation place_id "+place.place_id);
		yield return new WaitForSeconds (0.05f);
		//Debug.Log ("latpos.distance "+latPos.distance);
		//Debug.Log("latpos.storemeter "+latPos.store_meter);
		distance = System.Convert.ToInt32(latPos.distance - latPos.store_meter);
		if (distance > latPos.store_meter) {
			PopupManager.instance.ClosePopup();
			yield return new WaitForSeconds (0.3f);
			PopupManager.instance.OpenAlert ("คุณอยู่ห่างจาก "+place.place_name+" "+distance+" เมตร ไม่สามารถ checkin ");

		}else{
			FindNearLocation(place);
		}
	}

	void FindNearLocation(PlaceFilter place)
	{
		LatLongPosition lat = DataManager.instance.latlongList.Find (p => p.place_id == place.place_id);
		ServiceRequest.instance.CheckinSaveRequest(DataManager.instance.GetMember().member_id, lat.pos_id);
	}

	private void Events_CheckinComplete(PlaceCheckin placeCheckin)
	{
		EFE_Base.instance.OpenPanelByIndex (Intent.Checkin);
	}
}
