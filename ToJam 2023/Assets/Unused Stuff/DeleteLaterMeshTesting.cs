using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLaterMeshTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] verticies = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        verticies[0] = Vector3.zero;
        verticies[1] = new Vector3(0, 1, 0);
        verticies[2] = new Vector3(1, 1, 0);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
