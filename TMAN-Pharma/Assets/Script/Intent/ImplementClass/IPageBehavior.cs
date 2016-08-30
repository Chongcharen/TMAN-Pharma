using UnityEngine;
using System.Collections;

interface IPageBehavior {
    void ShowPage();
    void LoadData();
    void UpdatePage();
    void SetPosition();
    void SlideLeft();
    void SlideRight();
}
