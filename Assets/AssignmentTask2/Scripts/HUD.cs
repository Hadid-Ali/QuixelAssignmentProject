using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public Slider lightSlider;

    private void Awake()
    {
        instance = this;
    }
}
