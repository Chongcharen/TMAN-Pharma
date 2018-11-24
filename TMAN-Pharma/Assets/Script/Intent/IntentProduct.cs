using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Service.ClassReference;
public class IntentProduct : AbstractIntent {
    [SerializeField]
    Transform content;
    [SerializeField]
    GameObject boardPrefab;
    GameObject newsPrefab;

    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
        ClearBoard();

        content.transform.localPosition = Vector3.zero;
        Events.instance.PageReady_Dispatch();
		Events.OnLoadHeaderFile += Events_OnLoadHeaderFile;
		StartCoroutine (DelayUpdateIntent ());
    }

    

	void OnDisable(){
		Events.OnLoadHeaderFile -= Events_OnLoadHeaderFile;
	}

	IEnumerator UpdateBoardPromotion()
    {
        List<HeaderFile> files = DataManager.instance.headerPDFList;
        GameObject go;
        foreach (HeaderFile f in files)
        {
            go = Instantiate(boardPrefab) as GameObject;
			go.GetComponent<ProductBoard>().SetData(f);
            go.transform.SetParent(content);
            go.transform.localScale = Vector3.one;
            
			yield return new WaitForSeconds (0.5f);
        }
    }
    void ClearBoard()
    {

		for (var i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

	IEnumerator DelayUpdateIntent(){
		yield return new WaitForSeconds (0.5f);

		if (DataManager.instance.headerPDFList == null || DataManager.instance.headerPDFList.Count <= 0) {
			ServiceRequest.instance.GetAllHeaderPDF();
		}
		else
		{
			Events_OnLoadHeaderFile (DataManager.instance.headerPDFList);
		}
	}


	void Events_OnLoadHeaderFile (List<HeaderFile> header)
	{
		if (DataManager.instance.GetMemberType() == 0)
		{
			StartCoroutine(UpdateBoardPromotion());
		}
		else
		{
			StartCoroutine(UpdateBoardPromotion());
		}
		PopupManager.instance.ClosePopup ();
	}
}
