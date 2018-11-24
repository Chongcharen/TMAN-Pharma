#if !UNITY_4_3 && !UNITY_4_5

using UnityEngine;
using UnityEngine.UI;
namespace InfinityCode.OnlineMapsExamples
{
    [AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/uGUICustomTooltipExample")]
    public class GUICustomTooltip : MonoBehaviour
    {
        public GameObject tooltipPrefab;
        public Canvas container;
        public RectTransform rectTransform;
        

        private OnlineMapsMarker marker;
        private GameObject tooltip;

        // Use this for initialization
        private void Start()
        {
            marker = OnlineMaps.instance.AddMarker(new Vector2(Random.Range(0, 10), Random.Range(0, 10)), "Hello Worlddddddddddddddddddddddddd");
            marker.OnDrawTooltip = delegate { };

            OnlineMaps.instance.OnUpdateLate += OnUpdateLate;
        }

        private void OnUpdateLate()
        {
            Debug.Log("OnUpdateLate");
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

            }
            else if (tooltip != null)
            {
                OnlineMapsUtils.DestroyImmediate(tooltip);
                tooltip = null;
            }
        }
    }
}

#endif