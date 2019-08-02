using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuixelTest.ExtrudeAssignment
{
    public class HUD : MonoBehaviour
    {
        public static HUD instance;

        public Slider lightSlider;

        public void Toggle_BtnEvent()
        {
            if (BasicMeshOperations.instance)
                BasicMeshOperations.instance.CreateCube();
        }

        public void LoadMenuScene()
        {
            UtilMethods.BackToMenu();
        }

        private void Awake()
        {
            instance = this;
        }
    }
}
