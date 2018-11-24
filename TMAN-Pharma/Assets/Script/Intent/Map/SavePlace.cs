using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Service.ClassReference;
[AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/uGUICustomTooltipExample")]
public class SavePlace : MonoBehaviour {
    public GameObject tooltipPrefab;
    public Canvas container;
    public RectTransform rectTransform;


    private OnlineMapsMarker marker;
    private GameObject tooltip;

    private void Start()
    {
       // OnlineMapsLocationService.instance.OnLocationChanged += OnLocationChanged;
        //OnlineMaps.instance.OnUpdateLate += OnUpdateLate;
    }
    void OnEnable()
    {
		OnlineMaps.instance.RemoveAllMarkers();
		StartCoroutine (OpenLocation());

    }

	IEnumerator OpenLocation(){
		yield return new WaitForSeconds (0.01f);
		OnlineMapsLocationService.instance.OnLocationChanged += OnLocationChanged;
		OnlineMapsLocationService.instance.UpdatePosition();
		OnlineMapsLocationService.instance.restoreAfter = 1;
		OnlineMaps.instance.OnUpdateLate += OnUpdateLate;
		if(DataManager.instance.currentPosition != null || DataManager.instance.currentPosition != Vector2.zero)
		{
			OnLocationChanged(DataManager.instance.currentPosition);
		}
	}
    void OnDisable()
    {
		OnlineMaps.instance.RemoveAllDrawingElements ();
        OnlineMapsLocationService.instance.OnLocationChanged -= OnLocationChanged;
        OnlineMaps.instance.OnUpdateLate -= OnUpdateLate;
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
            tooltip.GetComponentInChildren<Text>().text = marker.label;
            tooltip.transform.localScale = Vector3.one;

        }
        else if (tooltip != null)
        {
            OnlineMapsUtils.DestroyImmediate(tooltip);
            tooltip = null;
        }
    }

  
    void OnLocationChanged(Vector2 location)
    {
        OnlineMaps.instance.RemoveAllMarkers();
        Place place = DataManager.instance.getPlace();
        if (place != null)
        {
            DataManager.instance.currentPosition = location;
        }
        marker = OnlineMaps.instance.AddMarker(location, place.place_name);
        marker.OnDrawTooltip = delegate { };
		OnlineMapsLocationService.instance.restoreAfter = 1;
		//OnlineMapsLocationService.instance.OnLocationChanged -= OnLocationChanged;
        
    }
}
