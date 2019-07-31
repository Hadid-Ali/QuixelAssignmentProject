using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public Slider lightSlider;

    public void Toggle_BtnEvent()
    {
        if (BasicMeshOperations.instance)
            BasicMeshOperations.instance.CreateCube();
    }

    private void Awake()
    {
        instance = this;
    }
}
