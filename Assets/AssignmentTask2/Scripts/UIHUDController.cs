using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuixelTest.CircleShaderAssignment
{
    public class UIHUDController : MonoBehaviour
    {
        public Slider radius, BGWidth, FGWidth;
        public MeshRenderer circle;

        public string Radius, BackgroundCutoff, ForegroundCutoff;


        private void Start()
        {
            this.radius.value = this.circle.material.GetFloat(this.Radius);
            this.BGWidth.value = this.circle.material.GetFloat(this.BackgroundCutoff);
            this.FGWidth.value = this.circle.material.GetFloat(this.ForegroundCutoff);
        }

        public void LoadBackMenu()
        {
            UtilMethods.BackToMenu();
        }

        public void SetValue(int i)
        {
            string attribute = "";
            float value = 0;

            attribute = i == 1 ? this.ForegroundCutoff : i == 2 ? this.BackgroundCutoff : this.Radius;
            value = i == 1 ? this.FGWidth.value : i == 2 ? this.BGWidth.value : this.radius.value;

            this.circle.material.SetFloat(attribute, value);
        }
    }
}