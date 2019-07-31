using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOperation : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private List<GameObject> triangles = new List<GameObject>();
    public List<GameObject> PolyGons = new List<GameObject>();

    public Light directionalLight;
    [Range(10,100)]
    public float senstivity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.ExtrudeMesh();
       // this.meshFilter.mesh = this.CalculateSideExtrusion(this.meshFilter.mesh);
    }

    Mesh CalculateSideExtrusion(Mesh mesh)
    {
        List<int> indices = new List<int>(mesh.triangles);
        int count = (mesh.vertices.Length / 2);
        for (int i = 0; i < count; i++)
        {
            int i1 = i;
            int i2 = (i1 + 1) % count;
            int i3 = i1 + count;
            int i4 = i2 + count;

            indices.Add(i4);
            indices.Add(i3);
            indices.Add(i1);

            indices.Add(i2);
            indices.Add(i4);
            indices.Add(i1);
        }
        mesh.triangles = indices.ToArray();
        return mesh;
    }

    public void ExtrudeMesh()
    {
        Mesh M = this.meshFilter.mesh;
        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;

        for (int subMesh = 0; subMesh < M.subMeshCount; subMesh++)
        {
            int[] indices = M.GetTriangles(subMesh);
            Debug.LogError(indices.Length);
            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];

                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };
                GameObject GO = new GameObject();
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.AddComponent<MeshRenderer>().material = meshRenderer.materials[subMesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                this.triangles.Add(GO);
            }
        }
        this.meshRenderer.enabled = false;

        int j = 0;

        for (int i = 0; i < this.triangles.Count - 1; i += 2)
        {
            GameObject halfPoly1 = this.triangles[i];
            GameObject halfPoly2 = this.triangles[i + 1];

            halfPoly2.transform.SetParent(halfPoly1.transform);
            halfPoly1.AddComponent<BoxCollider>();

            halfPoly1.name = "Polygon" + (j++).ToString();

            this.PolyGons.Add(halfPoly1);
        }

        //this.meshFilter.combin

       //Destroy(this.meshFilter.gameObject);
    }


    Vector3 normal = Vector3.zero;
    Vector3 pos = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) & !Input.GetKey(KeyCode.LeftControl))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                this.pos = hit.transform.position;
                this.normal = hit.normal;

                hit.transform.position += hit.normal;
            }
        }

        if (this.normal != Vector3.zero)
        {
            Debug.DrawRay(this.pos, this.normal * 10f, Color.red);
        }

        if (this.directionalLight)
        {
            this.directionalLight.intensity += this.senstivity * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
            this.directionalLight.intensity = Mathf.Clamp(this.directionalLight.intensity, HUD.instance.lightSlider.minValue, HUD.instance.lightSlider.maxValue);
            HUD.instance.lightSlider.value = this.directionalLight.intensity;
        }
    }
}
