using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

    public static MapManager instance;
    public GameObject mapDirection,findDirection,mapSavePlace;
    void Awake()
    {
        instance = this;
    }

    public void OpenMapDirection()
    {
        mapSavePlace.SetActive(false);
        mapDirection.SetActive(true);
        findDirection.SetActive(true);

    }
    public void OpenSavePlace()
    {
        findDirection.SetActive(false);
        mapDirection.SetActive(true);
        mapSavePlace.SetActive(true);
    }
    public void CloseMap()
    {
        findDirection.SetActive(false);
        mapDirection.SetActive(false);
        mapSavePlace.SetActive(false);
    }
}
