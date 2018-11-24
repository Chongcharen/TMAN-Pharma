using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InfinityCode.OnlineMapsExamples;
public class FindPlaceTooltipPopup : MonoBehaviour {

    public Text detail_txt, shopname_txt;
    public Button b_findDirection, b_tel;
    // Use this for initialization
    void Start() {
        b_findDirection.onClick.AddListener(OnFindDirection);
        b_tel.onClick.AddListener(OnTel);
    }
    void OnEnable()
    {
        shopname_txt.text = DataManager.instance.placeSelect.place_name;
        detail_txt.text = DataManager.instance.placeSelect.place_address;
    }
    void OnDisable()
    {
        Destroy(this.gameObject);
    }
    void OnFindDirection()
    {
         //OnlineMapsGoogleAPIQuery query = OnlineMapsGoogleDirections.Find(DataManager.instance.targetPosition,
                 //new Vector2(DataManager.instance.currentPosition.x, DataManager.instance.currentPosition.y));
       /* OnlineMapsGoogleAPIQuery query = OnlineMapsGoogleDirections.Find(DataManager.instance.placeSelect.place_address,
                                                                         "544 ถ.ท้ายบ้าน ต.ปากน้ำ อ.เมือง จ.สมุทรปราการ");
         // Specifies that search results must be sent to OnFindDirectionComplete.
         query.OnComplete += OnFindDirectionComplete;*/
        Application.OpenURL("https://maps.google.com?saddr=Current+Location&daddr=" + DataManager.instance.placeSelect.place_address);

    }
    void OnTel()
    {
		Application.OpenURL ("tel://+"+DataManager.instance.placeSelect.place_tel); 
    }

  
    void OnFindDirectionComplete(string response)
    {
        // Get the route steps.
        Debug.Log("response " + response);
        List<OnlineMapsDirectionStep> steps = OnlineMapsDirectionStep.TryParse(response);
        if (steps != null)
        {
            // Showing the console instructions for each step.
            foreach (OnlineMapsDirectionStep step in steps) Debug.Log(step.stringInstructions);

            // Get all the points of the route.
            List<Vector2> points = OnlineMapsDirectionStep.GetPoints(steps);
            Debug.Log("points count " + points.Count);
            // Create a line, on the basis of points of the route.
            OnlineMapsDrawingLine route = new OnlineMapsDrawingLine(points, Color.green);

            // Draw the line route on the map.
            OnlineMaps.instance.AddDrawingElement(route);
        }
        else
        {
            Debug.Log("Find direction failed");
        }
    }


}
