using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntentLuckyDraw : AbstractIntent {
    
    public Button b_submitAward;
    
    public Text luckyDrawCode, award_date_txt, ep_txt;

	public Text_Extend text_ex;

    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_submitAward.onClick.AddListener(OnSubmitAward);
    }
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        
        Events.instance.PageReady_Dispatch();
		Events.OnLoadLuckyCode += Events_OnLoadLuckyCode;
		StartCoroutine (DelayUpdateIntent ());
		text_ex.text = "";
		text_ex.OriginText = "";
    }

	void OnDisable(){
		Events.OnLoadLuckyCode -= Events_OnLoadLuckyCode;
	}
    
    void OnSubmitAward()
    {
        EFE_Base.instance.OpenPanelByIndex(Intent.LuckyDrawSendData);
    }


	IEnumerator DelayUpdateIntent(){
		yield return new WaitForSeconds (0.1f);
		/*
		if (DataManager.instance.luckyCode == null || DataManager.instance.luckyCode.Count <= 0) {
			ServiceRequest.instance.GetCodeAward ();
		} else {
			UpdateLuckyCode ();
		}*/

		ServiceRequest.instance.GetCodeAward ();
	}

	void UpdateLuckyCode(){
		luckyDrawCode.text = DataManager.instance.luckyCode[0].lucky_code;
		award_date_txt.text = "วันที่ "+DataManager.instance.luckyCode[0].award_date;
		ep_txt.text = "ครั้งที่ "+DataManager.instance.luckyCode[0].award_episode;
		text_ex.text = DataManager.instance.luckyCode[0].award_content;
		text_ex.OriginText = DataManager.instance.luckyCode[0].award_content;
	}

	void Events_OnLoadLuckyCode ()
	{
		UpdateLuckyCode ();
	}
}
