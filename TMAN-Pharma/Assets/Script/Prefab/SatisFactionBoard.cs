using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Service.ClassReference;
using System.Linq;
public class SatisFactionBoard : MonoBehaviour {

    [SerializeField]
    Text title_txt;
    ToggleGroup toggleGroup;
    List<Toggle> toggles;
	// Use this for initialization
	public void SetData(int index , SatisFactionTitle title)
    {
      //  title_txt.text = "" + index + ". " + title.title_name;

        toggleGroup = GetComponentInChildren<ToggleGroup>();
        toggles = GetComponentsInChildren<Toggle>().ToList();
    }
    public ToggleGroup GetToggleGroup() {

        return toggleGroup;
    }
    public int GetActiveToggleIndex()
    {
        return toggles.IndexOf(toggleGroup.ActiveToggles().FirstOrDefault());
    }
}
