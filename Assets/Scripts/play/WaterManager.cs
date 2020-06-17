using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class WaterManager : MonoBehaviour
{

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        Vector3[] normals = new Vector3[vertices.Length];


        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x);
            normals[i] = new Vector3(Mathf.Tan(vertices[i].y), Mathf.Tan(vertices[i].x), 0);

        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.SetNormals(normals);
        meshFilter.mesh.RecalculateNormals();

    }
}
