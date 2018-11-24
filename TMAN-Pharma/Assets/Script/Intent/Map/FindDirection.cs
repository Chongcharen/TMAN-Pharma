using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Service.ClassReference;
public class FindDirection : MonoBehaviour {
    public GameObject tooltipPrefab;
    public Canvas container;
    public RectTransform rectTransform;


    private OnlineMapsMarker marker;
    private GameObject tooltip;
    private OnlineMapsMarker searchMarker;
    protected OnlineMaps api;
    OnlineMapsGoogleAPIQuery query;

	double lat;
	double lng;
    void Start()
    {
        api = OnlineMaps.instance;
        //OnlineMapsFindLocation.Find(DataManager.instance.placeSelect.place_address).OnComplete += OnFindLocationComplete;

    }
    void OnEnable()
    {
		OnlineMaps.instance.RemoveAllMarkers();
		StartCoroutine ("StartFindPosition");

    }
	IEnumerator StartFindPosition(){
		yield return new WaitForSeconds (0.03f);


			LatLongPosition position = DataManager.instance.latlongList.Find(p => p.place_id == DataManager.instance.placeSelect.place_id );
        Debug.Log("StartfindDirection " + position);
		if (position == null) {
			lat = DataManager.instance.placeSelect.pos_latitude;
			lng = DataManager.instance.placeSelect.pos_longitude;
		} else {
			lat = position.pos_latitude;
			lng = position.pos_longitude;
		}
			Vector2 posLatlng = new Vector2((float)lat,(float)lng);
			OnlineMapsLocationService.instance.UpdatePosition();
			OnlineMapsLocationService.instance.restoreAfter = 1;
            OnlineMapsGoogleGeocoding.Find(DataManager.instance.placeSelect.place_address).OnComplete += OnFindLocationComplete;
			// OnlineMapsLocationService.instance.OnLocationChanged += OnLocationChanged;
			OnlineMaps.instance.OnUpdateLate += OnUpdateLate;

	}
    void OnDisable()
    {
       // OnlineMapsLocationService.instance.OnLocationChanged -= OnLocationChanged;
        OnlineMaps.instance.OnUpdateLate -= OnUpdateLate;
        api.RemoveAllDrawingElements();
        MapManager.instance.CloseMap();
    }

    private void OnUpdateLate()
    {
        if (marker == null) return;
        OnlineMapsMarkerBase tooltipMarker = OnlineMaps.instance.tooltipMarker;
        if (tooltipMarker != marker)
        {
            if (tooltip == null)
            {
                tooltip = Instantiate(tooltipPrefab);
                (tooltip.transform as RectTransform).SetParent(rectTransform.transform);
            }

            Vector2 screenPosition = OnlineMapsControlBase.instance.GetScreenPosition(marker.position);
            screenPosition.y += marker.height;
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.transform as RectTransform, screenPosition, null, out point);
            Vector2 newPoint = new Vector2(point.x, 100 + point.y);
            point = newPoint;
           
            (tooltip.transform as RectTransform).localPosition = point;

            tooltip.transform.localScale = Vector3.one;
            //marker = new Rect(point, Vector2.one);
            // tooltip.GetComponentInChildren<Text>().text = marker.label;

        }
        else if (tooltip != null)
        {
            OnlineMapsUtils.DestroyImmediate(tooltip);
            tooltip = null;
        }
    }


    void OnLocationChanged(Vector2 location)
    {
         //searchMarker = OnlineMaps.instance.AddMarker(location);
        DataManager.instance.currentPosition = location;
		if (DataManager.instance.isFindDirection) {
			OnlineMapsLocationService.instance.restoreAfter = 280;
		}
	
		//OnlineMapsLocationService.instance.OnLocationChanged -= OnLocationChanged;
        /* marker.OnDrawTooltip = delegate { };
         Place place = DataManager.instance.getPlace();
         if (place != null)
         {
             DataManager.instance.LatLogPosition = location;
         }*/
        //marker = OnlineMaps.instance.AddMarker(new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude));
    }

    private void OnFindLocationComplete(string result)
    {
        Debug.Log("result " + result);
        DataManager.instance.targetPosition = OnlineMapsGoogleGeocoding.GetCoordinatesFromResult(result);
        Debug.Log("DataManager.instance.targetPosition " + DataManager.instance.targetPosition.x + " , " + DataManager.instance.targetPosition.y);
        marker = OnlineMaps.instance.AddMarker(new Vector2((float)DataManager.instance.targetPosition.x, (float)DataManager.instance.targetPosition.y), DataManager.instance.placeSelect.place_name);
        marker.OnDrawTooltip = ( marker =>{
          

        });
        if (DataManager.instance.currentPosition != null)
        {
            OnLocationChanged(DataManager.instance.currentPosition);
        }
        if (api.zoom < 13) api.zoom = 13;

        api.position = DataManager.instance.targetPosition;
        api.Redraw();

    }
}
