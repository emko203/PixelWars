using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextController : MonoBehaviour
{
    private static FloatingText PopupTextPrefabRed;
    private static FloatingText PopupTextPrefabGreen;
    private static GameObject mainUI;

    public static void Initialize()
    {
        PopupTextPrefabRed = Resources.Load<FloatingText>("DamageNumbers/PopupTextParentRed");
        PopupTextPrefabGreen = Resources.Load<FloatingText>("DamageNumbers/PopupTextParentGreen");
        mainUI = GameObject.FindGameObjectWithTag("MAIN_UI");
    }

    public static void CreateFloatingText(string text, Transform location, bool green = false)
    {
        FloatingText instance;

        if (green)
        {
            instance = Instantiate(PopupTextPrefabGreen);
        }
        else
        {
            instance = Instantiate(PopupTextPrefabRed);
        }

        Vector3 textLocation = Camera.main.WorldToScreenPoint(new Vector3(location.position.x,location.position.y, -2));
        instance.transform.position = textLocation;
        instance.transform.SetParent(mainUI.transform);
        instance.SetText(text);
    }
}
