using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectSpawner : MonoBehaviour
{
    int br_tocaka;
    int br_trokuta;
    [SerializeField, HideInInspector]
    MeshFilter meshFilter;
    GenerateMesh planeMesh;

    public Material terrainMat;

    [Range(0, 7)]
    public int gustoca;

    [Range(10, 100)]
    public float size;

    [Range(0, 1)]
    public float offset;

    [Range(1, 20)]
    public float randomOffset;

    // Start is called before the first frame update
    private void OnValidate()
    {
        br_tocaka = Generate(gustoca,0);
        br_trokuta = (2*(br_tocaka-1)) * ((br_tocaka-1));
        Init();
        Generate();
    }

    void Init(){
        if (meshFilter == null)
            {
                meshFilter = new MeshFilter();
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilter = meshObj.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = new Mesh();
                
        }
        meshFilter.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;

        planeMesh = new GenerateMesh(br_tocaka, br_trokuta, gustoca, meshFilter, size, offset, randomOffset);
    }

    int Generate(int n,int rezultat){
        if (n == 0){
            return rezultat + 2;
        }
        return Generate(n-1, rezultat + (int)Math.Pow(2,n-1));

    }

    void Generate()
    {
        planeMesh.CreatePlane();
    }
}
