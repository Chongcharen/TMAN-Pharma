using UnityEngine;
using System.Collections;
using OneP.InfinityScrollView;
public class IntentPlaceResult : AbstractIntent {
    [SerializeField]
    GameObject boardPrefab;
    [SerializeField]
    Transform content;
	public InfinityScrollView scroll;
    public override void UpdatePage()
    {
        if (!VariableManager.GetInstance.canDispatchListener)
            return;
            ClearBoard();
            

        
        Events.instance.PageReady_Dispatch();
		StartCoroutine (DelayUpdateIntent ());
		content.transform.localPosition = Vector3.zero;
    }
    void OnDisable()
    {
		scroll.ClearObject ();
		scroll.Reset ();
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
      /*  for(int i = 0; i < DataManager.instance.placeFilter.Count; i++)
        {
            obj = Instantiate(boardPrefab, content) as GameObject;
            //obj.GetComponent<PlaceBoard>().UpdateData(DataManager.instance.placeFilter[i]);
            obj.transform.SetParent(content);
            obj.transform.localScale = Vector3.one;
        }*/
		scroll.Setup (DataManager.instance.placeFilter.Count);
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
