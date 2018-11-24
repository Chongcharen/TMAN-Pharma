using UnityEngine;
using System.Collections;

public class IntentShopFindResult : AbstractIntent {
	[SerializeField]
	GameObject boardPrefab;
	[SerializeField]
	Transform content;

	public override void UpdatePage()
	{
		if (!VariableManager.GetInstance.canDispatchListener)
			return;
		ClearBoard();


		content.transform.localPosition = Vector3.zero;
		Events.instance.PageReady_Dispatch();
		StartCoroutine (DelayUpdateIntent ());
	}

	void OnDisable()
	{
		ClearBoard();
	}
	void ClearBoard()
	{
		for(int i = 0; i < content.childCount; i++)
		{
			Destroy(content.GetChild(i).gameObject);
		}
		content.DetachChildren();
	}
	void GenerateBoard()
	{
		GameObject obj;
		for(int i = 0; i < DataManager.instance.branchPlace.Count; i++)
		{
			obj = Instantiate(boardPrefab, content) as GameObject;
			obj.GetComponent<BranchShopBoard>().UpdateData(DataManager.instance.branchPlace[i]);
			obj.transform.SetParent(content);
			obj.transform.localScale = Vector3.one;
		}
	}
	IEnumerator DelayUpdateIntent(){
		PopupManager.instance.OpenLoading ();
		yield return new WaitForSeconds (1);
		GenerateBoard();
		PopupManager.instance.ClosePopup ();
	}

}
