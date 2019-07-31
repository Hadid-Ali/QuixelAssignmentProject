using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOperation : BasicMeshOperations
{
    private List<GameObject> triangles = new List<GameObject>();
    public List<GameObject> PolyGons = new List<GameObject>();

    public override void Start()
    {
        base.Start();
        this.ExtrudeMesh();
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
        this.meshCollider.enabled = false;

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

    public void ScaleSiblingParts(GameObject part1,GameObject part2,Vector3 hitNormal,float scaleSenstivity,float positionSenstivity)
    {
        List<GameObject> list = new List<GameObject>();

        for(int i=0;i<this.PolyGons.Count;i++)
        {
            if (this.PolyGons[i] != part1 & this.PolyGons[i] != part2)
            {
                list.Add(this.PolyGons[i]);
                this.PolyGons[i].transform.localScale += hitNormal * scaleSenstivity;
                this.PolyGons[i].transform.position += hitNormal * positionSenstivity;
            }
        }

    }

    public float scaleSenstivity = 0.1f, positionSenstivity = 0.1f;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) & !Input.GetKey(KeyCode.LeftControl))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            bool canHit = Physics.Raycast(ray, out hit, 100f);

            //if(canHit)
            //    this.ExtrudeMesh();

            if (Physics.Raycast(ray, out hit, 100f))
            {
                this.pos = hit.transform.position;
                this.normal = hit.normal;

                hit.transform.position += hit.normal * this.extrusionSenstivity;

                RaycastHit u;

                if(Physics.Raycast(hit.point,-hit.normal,out u))
                {
                    Debug.LogError(u.transform.gameObject);
                    this.ScaleSiblingParts(hit.transform.gameObject, u.transform.gameObject, hit.normal, this.scaleSenstivity, this.positionSenstivity);
                }

                //CombineInstance[] instances = new CombineInstance[this.triangles.Count];

                //for (int i = 0; i < instances.Length; i++)
                //{
                //    instances[i].mesh = this.triangles[i].GetComponent<MeshFilter>().mesh;
                //    instances[i].transform = this.triangles[i].GetComponent<MeshRenderer>().transform.localToWorldMatrix;
                //}

                //for(int i=0;i<this.triangles.Count;i++)
                //{
                //    Destroy(this.triangles[i]);
                //}


                //Mesh m = new Mesh();
                //m.CombineMeshes(instances, true, true);

                //this.meshFilter.mesh = m;
            }
        }

        if (this.normal != Vector3.zero)
        {
            Debug.DrawRay(this.pos, this.normal * 10f, Color.red);
        }
    }
}
