using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuixelTest.ExtrudeAssignment
{
    public class BasicMeshOperations : MonoBehaviour
    {
        public static BasicMeshOperations instance;


        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;
        public MeshCollider meshCollider;

        public Light directionalLight;
        [Range(20f, 100)]
        public float senstivity = 10f;
        [Range(0.1f, 5f)]
        public float extrusionSenstivity = 1f;

        protected Vector3 normal = Vector3.zero;
        protected Vector3 pos = Vector3.zero;

        public virtual void Awake()
        {
            instance = this;
        }

        public virtual void Start()
        {
            this.CreateCube();
            this.SetDirectionalLightValue();
        }

        public virtual void CreateCube()
        {
            if (this.meshFilter)
                Destroy(this.meshFilter.gameObject);

            GameObject G = GameObject.CreatePrimitive(PrimitiveType.Cube);
            G.name = "T";
            this.meshFilter = G.GetComponent<MeshFilter>();
            this.meshRenderer = G.GetComponent<MeshRenderer>();

            if (this.meshRenderer.GetComponent<BoxCollider>())
                DestroyImmediate(this.meshRenderer.GetComponent<BoxCollider>());

            this.meshCollider = G.AddComponent<MeshCollider>();
        }

        public void SetDirectionalLightValue()
        {
            if (this.directionalLight)
            {
                this.directionalLight.intensity = HUD.instance.lightSlider.value;
            }
        }

        public virtual void Update()
        {
            if (this.directionalLight)
            {
                this.directionalLight.intensity += this.senstivity * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
                this.directionalLight.intensity = Mathf.Clamp(this.directionalLight.intensity, HUD.instance.lightSlider.minValue, HUD.instance.lightSlider.maxValue);
                HUD.instance.lightSlider.value = this.directionalLight.intensity;
            }
        }
    }
}
