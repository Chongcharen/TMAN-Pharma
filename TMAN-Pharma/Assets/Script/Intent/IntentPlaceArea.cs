using UnityEngine;
using System.Collections;
using OneP.InfinityScrollView;
public class IntentPlaceArea : AbstractIntent {

	[SerializeField]
	GameObject boardPrefab;
	[SerializeField]
	Transform content;
	public InfinityScrollView scroll;
	public override void UpdatePage()
	{
		if (!VariableManager.GetInstance.canDispatchListener)
			return;
		scroll.ClearObject ();
		scroll.Reset ();



		Events.instance.PageReady_Dispatch();
		StartCoroutine (DelayUpdateIntent ());
		content.transform.localPosition = Vector3.zero;
	}
	void OnDisable()
	{
		scroll.ClearObject ();
		scroll.Reset ();
	}
	void GenerateBoard()
	{
		GameObject obj;
		scroll.Setup (DataManager.instance.placeFilterArea.Count);
		scroll.InternalReload ();
		content.transform.localPosition = Vector2.zero;
	}
	IEnumerator DelayUpdateIntent(){
		PopupManager.instance.OpenLoading ();
		yield return new WaitForSeconds (1);
		GenerateBoard();
		PopupManager.instance.ClosePopup ();
	}
}
