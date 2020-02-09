using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapGen : MonoBehaviour
{
    private MeshFilter thisMesh;
    private MeshCollider mCollide;

    public float maxHeight = 2;
    public float minHeight = 0.5f;

    void Awake()
    {
        thisMesh = GetComponent<MeshFilter>();
        mCollide = GetComponent<MeshCollider>();
    }

    public void generateHeightMap() {
        float seed = Random.value * 100;
        float height = Random.Range(minHeight, maxHeight);
        Vector3[] vertices = thisMesh.mesh.vertices;
        int verticesLength = (int)Mathf.Sqrt(vertices.Length);
        for(int i = 0; i < verticesLength; i++) {
            for(int j = 0; j < verticesLength; j++) {
                if(i == 0 || j == 0 || i == verticesLength - 1 || j == verticesLength - 1) {
                    continue;
                }
                vertices[(i * (verticesLength)) + j].y = Mathf.PerlinNoise(seed + (float)i, seed + (float)j) * height;
            }
        }
        thisMesh.mesh.vertices = vertices;
        thisMesh.mesh.RecalculateBounds();
        thisMesh.mesh.RecalculateNormals();
        mCollide.sharedMesh = thisMesh.mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
