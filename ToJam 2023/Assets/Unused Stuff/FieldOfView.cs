using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FieldOfView : MonoBehaviour
{
    public float fieldOfView = 450;
    public float angle = 0;
    public float viewDistance =1;
    public int ammountOfRays = 30;

    public GameObject player;

    private Vector3 originOfRaycast;

    private Mesh mesh;
    private SpriteMask mask;
    private Sprite sprite;



    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mask = new SpriteMask();
        sprite = GetComponent<SpriteMask>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        //OverrideSprite();

        originOfRaycast = player.transform.position;
    
        float angleIncrease = fieldOfView / ammountOfRays;


        Vector3[] vertices = new Vector3[ammountOfRays + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[ammountOfRays * 3];


        vertices[0] = originOfRaycast;


        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0;i < ammountOfRays; i++)
        {
            float angleInRadians = angle * (MathF.PI/ 180);

            Vector3 vertex;

            
            RaycastHit2D raycastHit2D = Physics2D.Raycast(originOfRaycast, new Vector3(MathF.Cos(angleInRadians), Mathf.Sin(angleInRadians)), viewDistance);
            if (raycastHit2D.collider == null)
            {
                //no hit
                vertex = originOfRaycast + new Vector3(MathF.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * viewDistance;
            } else
            {
                //hit something
                vertex = raycastHit2D.point;
            }
            

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
            
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;



        

        Vector2[] copiedVerticies = new Vector2[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            copiedVerticies[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].y);
        }
        for (int i = 0; i < copiedVerticies.Length; ++i)
            copiedVerticies[i] = (copiedVerticies[i] * sprite.pixelsPerUnit) + sprite.pivot;
        ushort[] copiedTriangels = new ushort[mesh.triangles.Length];
        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            copiedTriangels[i] = (ushort)mesh.triangles[i];
        }
        sprite.OverrideGeometry(copiedVerticies, copiedTriangels);
       
    }

    private void FixedUpdate()
    {
        
    }
}
