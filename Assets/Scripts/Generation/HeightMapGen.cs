using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapGen : MonoBehaviour
{
    private MeshFilter thisMesh;
    private MeshCollider mCollide;
    void Awake()
    {
        thisMesh = GetComponent<MeshFilter>();
        mCollide = GetComponent<MeshCollider>();
    }

    public void generateHeightMap() {
        Vector3[] vertices = thisMesh.mesh.vertices;
        int verticesLength = (int)Mathf.Sqrt(vertices.Length);
        for(int i = 0; i < verticesLength; i++) {
            for(int j = 0; j < verticesLength; j++) {
                vertices[(i * j) + j].y = Mathf.Abs(Mathf.PerlinNoise(i/ 10, j / 10) * 1.5f);
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
