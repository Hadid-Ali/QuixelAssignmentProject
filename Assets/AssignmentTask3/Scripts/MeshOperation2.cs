using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOperation2 : BasicMeshOperations
{
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if ((!Input.GetKey(KeyCode.LeftControl) | !Input.GetKey(KeyCode.RightAlt)) & Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                this.pos = hit.transform.position;
                this.normal = hit.normal ;

                GameObject G = Instantiate(this.meshFilter.gameObject);
                G.transform.position = this.meshFilter.transform.position + (hit.normal / (20f-this.extrusionSenstivity));
                G.transform.rotation = this.meshFilter.transform.rotation;
               //return;
                List<GameObject> list = new List<GameObject>();

                list.Add(this.meshFilter.gameObject);
                list.Add(G);

                CombineInstance[] instances = new CombineInstance[2];

                for (int i = 0; i < instances.Length; i++)
                {
                    instances[i].mesh = list[i].GetComponent<MeshFilter>().mesh;
                    instances[i].transform = list[i].GetComponent<MeshRenderer>().transform.localToWorldMatrix;
                }

                for(int i=1;i<list.Count;i++)
                {
                    Destroy(list[i]);
                }

                Mesh m = new Mesh();
                m.CombineMeshes(instances, true, true);

                this.meshFilter.GetComponent<MeshCollider>().sharedMesh = m;
                this.meshFilter.mesh = m;
            }
        }

        if (this.normal != Vector3.zero)
        {
            Debug.DrawRay(this.pos, this.normal * 10f, Color.red);
        }
    }
}
