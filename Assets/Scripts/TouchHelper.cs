using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchHelper
{
    #region ぜずぞそ

    public static bool Touch3 => Input.touchCount == 3 && (Input.GetTouch(2).phase == TouchPhase.Began);
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Began);
    public static bool IsDown => Input.GetTouch(0).phase == TouchPhase.Began; 
    public static bool IsUp => Input.GetTouch(0).phase == TouchPhase.Ended;
    public static Vector2 TouchPos => Input.GetTouch(0).position;

    #endregion
}
