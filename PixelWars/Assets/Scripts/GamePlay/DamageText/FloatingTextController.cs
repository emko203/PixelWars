using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    private static FloatingText PopupTextPrefab;
    private static GameObject mainUI;

    public static void Initialize()
    {
        PopupTextPrefab = Resources.Load<FloatingText>("DamageNumbers/PopupTextParent");
        mainUI = GameObject.FindGameObjectWithTag("MAIN_UI");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(PopupTextPrefab);
        Vector3 textLocation = Camera.main.WorldToScreenPoint(new Vector3(location.position.x,location.position.y, -2));
        instance.transform.position = textLocation;
        instance.transform.SetParent(mainUI.transform);
        instance.SetText(text);
    }
}
