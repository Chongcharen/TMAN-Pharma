using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Service.ClassReference;
public class Events : MonoBehaviour {
    public delegate void PageReadyEvent();
    public static event PageReadyEvent PageReady;

    public delegate void OpenLoaderEvent();
    public static event OpenLoaderEvent OpenLoader;

	public delegate void LoadInstanceDropdownEvent (Dropdown target);
	public static event LoadInstanceDropdownEvent LoadInstanceDropdown;

	public delegate void DropdownSelectEvent(Dropdown dropdown,int index);
	public static event DropdownSelectEvent DropdownSelect;

    public delegate void LoadPlaceCompleteEvent(Place place);
    public static event LoadPlaceCompleteEvent LoadPlaceComplete;

    public delegate void CheckinCompleteEvent(PlaceCheckin placeCheckin);
    public static event CheckinCompleteEvent CheckinComplete;

    public delegate void LoadRewardCodeCompleteEvent(string exp_code);
    public static event LoadRewardCodeCompleteEvent LoadRewardCodeComplete;

	public delegate void UpdateMemberPointEvent(string memberPoint);
	public static event UpdateMemberPointEvent OnUpdateMemberPoint;


	public delegate void LoadHeaderFileEvent(List<HeaderFile> header);
	public static event LoadHeaderFileEvent OnLoadHeaderFile;

	public delegate void LoadProfileEvent(MemberProfile profile);
	public static event LoadProfileEvent OnLoadMemberProfile;

	public delegate void LoadRecommendEvent(List<RecommendTitle> profile);
	public static event LoadRecommendEvent OnLoadRecommend;

	public delegate void LoadSatisFactionEvent();
	public static event LoadSatisFactionEvent OnLoadSatisFaction;

	public delegate void LoadLuckyCodeEvent();
	public static event LoadLuckyCodeEvent OnLoadLuckyCode;

	public delegate void LoadLocationEvent ();
	public static event LoadLocationEvent OnLoadLocation;

	public delegate void SearchPlaceCompleteEvent(PlaceFilter place);
	public static event SearchPlaceCompleteEvent OnSearchPlaceComplete;

	public delegate void SearchAreaPlaceCompleteEvent(List<PlaceFilter> places);
	public static event SearchAreaPlaceCompleteEvent OnSearchAreaPlaceComplete;

    public static Events instance;

    void Awake()
    {
        instance = this;
    }
    public void CheckinComplete_Dispatch(PlaceCheckin placeCheckin)
    {
        CheckinComplete(placeCheckin);
    }
    public void PageReady_Dispatch()
    {
        PageReady();
    }

    public void OpenLoader_Dispatch()
    {
        OpenLoader();
    }

	public void LoadInstanceDropdown_Dispatch(Dropdown target){
		LoadInstanceDropdown (target);
	}
	public void DropdownSelect_Dispatch(Dropdown target,int index){
		DropdownSelect (target,index);
	}

    public void LoadPlace_Dispatch(Place place)
    {
        LoadPlaceComplete(place);
    }
    public void LoadRewardCode_Dispatch(string exp_code)
    {
        LoadRewardCodeComplete(exp_code);
    }
	public void UpdateMemberPoint_Dispatch (string point){
		OnUpdateMemberPoint (point);
	}

	public void LoadHeaderFile_Dispatch(List<HeaderFile> header){
		OnLoadHeaderFile (header);
	}

	public void LoadMemberProfile_Dispatch(MemberProfile profile){
		OnLoadMemberProfile (profile);
	}

	public void LoadRecommend_Dispatch(List<RecommendTitle> recommend){
		OnLoadRecommend (recommend);
	}

	public void LoadSatis_Dispatch(){
		OnLoadSatisFaction ();
	}

	public void LoadLuckyCode_Dispatch(){
		OnLoadLuckyCode ();
	}
	public void LoadLocation_Dispatch(){
		OnLoadLocation ();
	}

	public void SearchPlace_Dispatch(PlaceFilter place){
		OnSearchPlaceComplete(place);
	}
	public void SearchAreaPlace_Dispatch(List<PlaceFilter> place){
		OnSearchAreaPlaceComplete(place);
	}


}
