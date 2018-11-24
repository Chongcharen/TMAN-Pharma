using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
using System.Linq;
public class IntentCheckin : AbstractIntent
{
    
    public Text name_txt, checkin_txt, date_txt, time_txt,code_txt,tip_txt;
    
    public Button b_getReward;
    
    public GameObject objCode;
    int place_id;
    void Start()
    {
        AddButtonListeners();
        b_getReward.onClick.AddListener(OnGetReward);
    }
    void OnEnable()
    {
        Events.LoadRewardCodeComplete += Events_LoadRewardCodeComplete;
        UpdatePage();
    }

   

    void OnDisable()
    {
        Events.LoadRewardCodeComplete -= Events_LoadRewardCodeComplete;
        objCode.SetActive(false);
		tip_txt.gameObject.SetActive (false);
    }
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
		Events.instance.PageReady_Dispatch();
		ShowPage ();
    }
	public override void ShowPage ()
	{
		name_txt.text = "คุณ " + DataManager.instance.placeCheckin.sername;
		checkin_txt.text = DataManager.instance.placeCheckin.place_name;
		date_txt.text = DataManager.instance.placeCheckin.date;
		time_txt.text = DataManager.instance.placeCheckin.time;
		place_id = DataManager.instance.placeCheckin.place_id;
    }
    private void Events_LoadRewardCodeComplete(string exp_code)
    {
        objCode.SetActive(true);
		tip_txt.gameObject.SetActive (true);
        code_txt.text = exp_code;
    }
    void OnGetReward()
    {
		Debug.Log ("DataManager.instance.memberProfile.member_id " + DataManager.instance.GetMember().member_id);
		ServiceRequest.instance.GetRewardPointRequest(place_id,DataManager.instance.GetMember().member_id);
    }
	public override void OnBack ()
	{
		EFE_Base.instance.OpenPanelByIndex (Intent.Menu);
		EFE_Base.instance.ClearHistory ();
	}


}
