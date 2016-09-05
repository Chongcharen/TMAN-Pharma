using UnityEngine;
using System.Collections;

interface IPageBehavior {
    void ShowPage();
    void LoadData();
    void UpdatePage();
    void SlideLeft(bool closeWhenComplete);
    void SlideRight(bool closeWhenComplete);
}
