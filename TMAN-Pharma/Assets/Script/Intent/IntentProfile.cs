using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VoxelBusters.Utility;
using VoxelBusters.Utility.UnityGUI.MENU;
using VoxelBusters.NativePlugins;
using Service.ClassReference;

public class IntentProfile : AbstractIntent {
    
    public Button b_save_profile, b_logout,b_send_codepoint;
    
    public Text member_code_txt, point_txt, type_txt,type_member_txt;
    
    public InputField name_input, tel1_input, tel2_input, tel3_input,email_input,fax_input,address_input,code_point_input;
   
    public Dropdown dropdown_province;
    
    public GameObject codePoint_obj, point_obj;

    MemberProfile member;
    List<Province> province;
	void Start(){
		AddButtonListeners ();
		Events.OnUpdateMemberPoint += Events_OnUpdateMemberPoint;
		Events.OnLoadMemberProfile += Events_OnLoadMemberProfile;
	}

	void OnDisable(){
		type_member_txt.text = "";
		type_txt.text = "";

		point_txt.text = "";
		member_code_txt.text = "";
		name_input.text = "";
		tel1_input.text = "";
		tel2_input.text = "";
		tel3_input.text = "";
		email_input.text = "";
		fax_input.text = "";
		address_input.text = "";

	}
    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_save_profile.onClick.AddListener(OnSaveProfile);
        b_logout.onClick.AddListener(OnLogout);
        b_send_codepoint.onClick.AddListener(OnSendCodePoint);
    }
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
       // UpdateMember();
        //AddOptionDataDropDown();
		CheckPagemember();
        Events.instance.PageReady_Dispatch();
		StartCoroutine (DelayUpdateIntent ());
    }
	void CheckPagemember(){
		if(DataManager.instance.GetMember().member_id == 1)
		{
			type_member_txt.text = "กลุ่มเขต";
			codePoint_obj.SetActive(false);
			point_obj.SetActive(false);
		}
		else
		{
			type_member_txt.text = "ประเภทลูกค้า";
			codePoint_obj.SetActive(true);
			point_obj.SetActive(true);
		}
	}
    void UpdateMember()
    {
       
        if (DataManager.instance.memberProfile!= null)
        {
            member = DataManager.instance.memberProfile;
        }
        if(member.member_type == 0)
        {
            type_member_txt.text = "กลุ่มเขต";
            codePoint_obj.SetActive(false);
            point_obj.SetActive(false);
            type_txt.text = member.group_name;
        }
        else
        {
            type_member_txt.text = "ประเภทลูกค้า";
            codePoint_obj.SetActive(true);
            point_obj.SetActive(true);
            type_txt.text = member.storename;
			point_txt.text = UIHelper.SetCurrencyWithoutK(System.Convert.ToSingle(member.member_point));
        }
        member_code_txt.text = member.member_code;
       
        

        name_input.text = member.sername;
        tel1_input.text = member.member_tel1;
        tel2_input.text = member.member_tel2;
        tel3_input.text = member.member_tel3;
        email_input.text = member.email;
        fax_input.text = member.member_fax;
        address_input.text = member.member_address;
    }
    void AddOptionDataDropDown()
    {

        Dropdown.OptionData optionData;
        dropdown_province.ClearOptions();
        province = DataManager.instance.province;
        foreach (Province p in province)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = p.PROVINCE_NAME;
            dropdown_province.value = member.province_id-1;
            dropdown_province.options.Add(optionData);
        }

        dropdown_province.RefreshShownValue(); 
     
    }
    void OnSaveProfile()
    {
        int store_id = 0;
        if(DataManager.instance.GetMemberType() != 0)
        {
            store_id = DataManager.instance.GetMember().store_id;
        }
        ServiceRequest.instance.UpdateMemberProfile(DataManager.instance.GetMember().member_id, name_input.text, tel1_input.text,
                                                    tel2_input.text, tel3_input.text, email_input.text, fax_input.text, dropdown_province.value + 1,
                                                    address_input.text, store_id);
    }
    void OnLogout()
    {
        DataManager.instance.Logout();
    }
    void OnSendCodePoint()
    {
		if (string.IsNullOrEmpty (code_point_input.text)) {
			PopupManager.instance.OpenAlert ("กรุณากรอกรหัสสะสมแต้ม");
			return;
		}
        ServiceRequest.instance.UpdatePointRequest(DataManager.instance.GetMember().member_id,code_point_input.text);
    }


	IEnumerator DelayUpdateIntent(){
		yield return new WaitForSeconds (1f);
		ServiceRequest.instance.GetMemberProfile(DataManager.instance.GetMember().member_id);
	}


	void Events_OnLoadMemberProfile (MemberProfile profile)
	{
		UpdateMember();
		AddOptionDataDropDown();
	}
	void Events_OnUpdateMemberPoint (string memberPoint)
	{
		int currentPoint = System.Convert.ToInt32(point_txt.text);
		int newPoint = System.Convert.ToInt32(memberPoint);
		int total = currentPoint + newPoint;
		if (!string.IsNullOrEmpty (memberPoint)) {
			point_txt.text = UIHelper.SetCurrencyWithoutK (System.Convert.ToSingle (total));
		}
	}
}
