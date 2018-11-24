using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
using UnityEngine.EventSystems;
public class IntentRegister : AbstractIntent
{
    
    public Button b_register;
    public InputField input_username, input_password, input_code, input_email, shop_name, input_name, input_tel1, input_tel2, input_tel3, input_fax, input_address;
    public Dropdown d_province, d_category;

    List<Province> province;
    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_register.onClick.AddListener(OnRegister);

    }
  
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        AddOptionDataDropDown();
        Events.instance.PageReady_Dispatch();
    }
    void AddOptionDataDropDown()
    {

        Dropdown.OptionData optionData;
        d_province.ClearOptions();
        d_category.ClearOptions();
        foreach (Province p in DataManager.instance.province)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = p.PROVINCE_NAME;
            d_province.options.Add(optionData);
        }
        foreach (Store d in DataManager.instance.store)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = d.store_name;
            d_category.options.Add(optionData);
        }

        d_province.RefreshShownValue();
        d_category.RefreshShownValue();

    }
    void OnRegister()
    {
		if (string.IsNullOrEmpty(input_username.text))
		{
			PopupManager.instance.OpenAlert("กรุณาระบุ username");
			return;
		}
		if (string.IsNullOrEmpty(input_password.text))
		{
			PopupManager.instance.OpenAlert("กรุณาระบุ password");
			return;
		}
		
		if (string.IsNullOrEmpty(input_email.text))
		{
			PopupManager.instance.OpenAlert("กรุณาระบุ อีเมล");
			return;
		}
        if (string.IsNullOrEmpty(shop_name.text))
        {
            PopupManager.instance.OpenAlert("กรุณาระบุ ชื่อร้าน");
            return;
        }
        if (string.IsNullOrEmpty(input_name.text))
        {
            PopupManager.instance.OpenAlert("กรุณาระบุ ชื่อ-นามสกุล");
            return;
        }
		if (string.IsNullOrEmpty(input_tel1.text))
		{
			PopupManager.instance.OpenAlert("กรุณาระบุ เบอร์โทรศัพท์");
			return;
		}
        if (string.IsNullOrEmpty(input_code.text))
        {
            PopupManager.instance.OpenAlert("กรุณาระบุ รหัสร้านค้า");
            return;
        }

        int provinceid = DataManager.instance.province[d_province.value].PROVINCE_ID;
        int store_id = DataManager.instance.store[d_category.value].store_id;
        ServiceRequest.instance.RegisterRequest(input_username.text,input_password.text,input_name.text,input_email.text,
                                                input_tel1.text,input_tel2.text,input_tel3.text,input_fax.text,input_address.text,
			provinceid, store_id,shop_name.text,input_code.text,"");
    }

}