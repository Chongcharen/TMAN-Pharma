using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Service.ClassReference;
public class IntentSatisFaction : AbstractIntent {
    [SerializeField]
    Transform content;
    public GameObject satisTiltleBoardPrefab,satisBoardPrefab,buttonPrefab;
    [SerializeField]
    List<SatisFactionBoard> satisBoards;
 
    List<Toggle> toggles1, toggles2, toggles3, toggles4, toggles5, toggles6, toggles7;
   
    int[] indexArray;
    int[] titleIndex;
    void Start()
    {
        AddButtonListeners();
    }
    void OnDisable()
    {
		Events.OnLoadSatisFaction -= Events_OnLoadSatisFaction;
    }
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
		//ClearBoard ();
        //GeneratePage();
        Events.instance.PageReady_Dispatch();
		Events.OnLoadSatisFaction += Events_OnLoadSatisFaction;
		StartCoroutine (DelayUpdateIntent());
    }
	void ClearBoard(){
		for (var i = 0; i < content.childCount; i++)
		{
			Destroy(content.GetChild(i).gameObject);
		}
		satisBoards.Clear ();
	}

    

    void GeneratePage()
    {
        GameObject go;
        int index = 1;
		titleIndex = new int[DataManager.instance.satisFactionData.title.Count];
        indexArray = new int[titleIndex.Length];
		if (DataManager.instance.satisFactionData.title_content.Length > 0) {
			go = Instantiate (satisTiltleBoardPrefab) as GameObject;
			go.transform.SetParent (content);
			go.transform.localScale = Vector3.one;
			go.GetComponent<SatisTitleBoard> ().SetData(DataManager.instance.satisFactionData.title_content);

		}

		foreach (SatisFactionTitle s in DataManager.instance.satisFactionData.title)
        {
            go = Instantiate(satisBoardPrefab) as GameObject;
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            go.GetComponent<SatisFactionBoard>().SetData(index,s);
            satisBoards.Add(go.GetComponent<SatisFactionBoard>());
            titleIndex[index - 1] = s.title_id;
            index++;
        }
       
		go = Instantiate(buttonPrefab) as GameObject;
		go.transform.SetParent(content);
		go.transform.localScale = Vector3.one;
		go.GetComponent<Button> ().onClick.AddListener (SendSatisData);
    }
    void SendSatisData()
    {
        int ind = 0;
        foreach(SatisFactionBoard b in satisBoards)
        {
            indexArray[ind] = b.GetActiveToggleIndex();
			switch (indexArray [ind]) {
			case 0:
				indexArray [ind] = 4;
				break;
			case 1:
				indexArray [ind] = 3;
				break;
			case 2:
				indexArray [ind] = 2;
				break;
			case 3:
				indexArray [ind] = 1;
				break;
			case 4:
				indexArray [ind] = 0;
				break;
			}

            ind++;
        }

        string titleResult = string.Join(",", Array.ConvertAll(titleIndex, i => i.ToString()));
        string toggleActiveResult = string.Join(",", Array.ConvertAll(indexArray, i => i.ToString()));
        ServiceRequest.instance.SendSatisSurvey(DataManager.instance.GetMember().member_id,titleResult, toggleActiveResult);
    }

	IEnumerator DelayUpdateIntent(){
		yield return new WaitForSeconds (1f);
		if (DataManager.instance.satisFactionData == null || DataManager.instance.satisFactionData.title.Count <= 0) {
			ServiceRequest.instance.GetTitleSurvey();
		} else {

			ClearBoard ();
			GeneratePage();
		}
	}

	void Events_OnLoadSatisFaction ()
	{
		ClearBoard ();
		GeneratePage();
	}
}
