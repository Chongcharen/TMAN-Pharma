using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Service.ClassReference;
public class IntentFindPlace : AbstractIntent {
    [SerializeField]
    Button b_find;
    [SerializeField]
    Dropdown dropdown_province;
    [SerializeField]
    InputField input_place_id;
    List<Province> province;
    void Start()
    {
        AddButtonListeners();
    }
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        province = DataManager.instance.province;
        AddOptionDataDropDown();
        Events.instance.PageReady_Dispatch();
    }
    void AddOptionDataDropDown()
    {
       
        Dropdown.OptionData optionData;
        dropdown_province.ClearOptions();
        foreach (Province p in province)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = p.PROVINCE_NAME;
            dropdown_province.options.Add(optionData);
        }

        dropdown_province.RefreshShownValue();
        dropdown_province.Show();
    }
    public override void AddButtonListeners()
    {
        base.AddButtonListeners();
        b_find.onClick.AddListener(OnFindPlace);
    }
    void OnFindPlace()
    {
		DataManager.instance.isFindDirection = true;
        ServiceRequest.instance.SearchPlaceRequest(province[dropdown_province.value].PROVINCE_ID, input_place_id.text);
    }
}
