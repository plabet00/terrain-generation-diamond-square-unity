using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateMesh
{
    MeshFilter meshFilter;
    int br_tocaka;
    int br_trokuta;
    int gustoca;
    float offset;
    float size;
    float randomOffset;
    public float min;
    public float max;
    
    public GenerateMesh(int br_tocaka, int br_trokuta, int gustoca, MeshFilter meshFilter, float size, float offset, float randomOffset){
        this.meshFilter = meshFilter;
        this.br_tocaka = br_tocaka;
        this.br_trokuta = br_trokuta;
        this.gustoca = gustoca;
        this.size = size;
        this.offset = offset;
        this.randomOffset = randomOffset;
    }

    private void setExtreme(float vertex)
    {
        if (vertex > max)
        {
            max = vertex;
        }
        else if (vertex < min)
        {
            min = vertex;
        }
    }

    public void CreatePlane(){
        Vector3[] vertices = new Vector3[br_tocaka * br_tocaka];
        Vector2[] uvs = new Vector2[br_tocaka * br_tocaka];
        int[] triangles = new int[br_trokuta*3];
        int br1 = 0;
        for(int i = 0; i < br_tocaka; i++){
            for(int j = 0; j < br_tocaka; j++){
                vertices[br1] = new Vector3(-size + j*(size/(br_tocaka-1)), 0 , size - i * (size / (br_tocaka - 1)));
                br1 += 1;
            }
        }
        int br2 = 0;
        for(int i = 0; i < br_tocaka * br_tocaka - br_tocaka; i = i + br_tocaka){
            for(int j = 0; j <= br_tocaka - 2; j++){
                triangles[br2] = j + i;
                triangles[br2+1] = j + 1 + i;
                triangles[br2+2] = j + br_tocaka + i;

                triangles[br2+3] = br_tocaka + j + i;
                triangles[br2+4] = j + 1 + i;
                triangles[br2+5] = j + br_tocaka + 1 + i;
                br2 += 6;
            }
        }
        

        int iterations = gustoca;
        int jump = br_tocaka;
        int numOfSquares = 1;
        vertices[0].y = randomOffset;
        min = vertices[0].y;
        max = vertices[0].y;
        vertices[br_tocaka].y = randomOffset;
        setExtreme(vertices[br_tocaka].y);
        vertices[br_tocaka * br_tocaka -1].y = randomOffset;
        setExtreme(vertices[br_tocaka * br_tocaka - 1].y);
        vertices[br_tocaka * br_tocaka - br_tocaka -1].y = randomOffset;
        setExtreme(vertices[br_tocaka * br_tocaka - br_tocaka - 1].y);
        for (int x = 0; x < iterations; x++)
        {
            for(int y = 0; y < numOfSquares; y++)
            {
                for (int z = 0; z < numOfSquares; z++)
                {
                    int currentIndex = (jump / 2) * br_tocaka + jump / 2 + z * jump + y * jump * br_tocaka;
                    int topLeft = currentIndex - br_tocaka * (jump / 2) - jump / 2;
                    int topRight = currentIndex - br_tocaka * (jump / 2) + jump / 2;
                    int bottomLeft = currentIndex + br_tocaka * (jump / 2) - jump / 2;
                    int bottomRight = currentIndex + br_tocaka * (jump / 2) + jump / 2;
                    vertices[currentIndex].y = (vertices[topLeft].y + vertices[topRight].y + vertices[bottomLeft].y + vertices[bottomRight].y) / 4 + UnityEngine.Random.Range(-randomOffset, randomOffset);
                    setExtreme(vertices[currentIndex].y);

                    vertices[currentIndex + jump/2].y = (vertices[currentIndex].y + vertices[topRight].y + vertices[bottomRight].y) / 3 + UnityEngine.Random.Range(-randomOffset, randomOffset);
                    setExtreme(vertices[currentIndex + jump / 2].y);
                    vertices[currentIndex - jump/2].y = (vertices[currentIndex].y + vertices[topLeft].y + vertices[bottomLeft].y) / 3 + UnityEngine.Random.Range(-randomOffset, randomOffset);
                    setExtreme(vertices[currentIndex - jump / 2].y);
                    vertices[currentIndex - br_tocaka * (jump / 2)].y = (vertices[currentIndex].y + vertices[topRight].y + vertices[topLeft].y) / 3 + UnityEngine.Random.Range(-randomOffset, randomOffset);
                    setExtreme(vertices[currentIndex - br_tocaka * (jump / 2)].y);
                    vertices[currentIndex + br_tocaka * (jump / 2)].y = (vertices[currentIndex].y + vertices[bottomRight].y + vertices[bottomLeft].y) / 3 +UnityEngine.Random.Range(-randomOffset, randomOffset);
                    setExtreme(vertices[currentIndex + br_tocaka * (jump / 2)].y);
                }
            }
            jump = jump / 2;
            numOfSquares *= 2;
            randomOffset *= offset;
        }

        for(int i = 0; i < br_tocaka; i++)
        {
            for (int j = 0; j < br_tocaka; j++)
            {
                uvs[i * br_tocaka + j] = new Vector2(vertices[i * br_tocaka + j].x, vertices[i * br_tocaka + j].z);
            }
        }

        meshFilter.sharedMesh.Clear();
        meshFilter.sharedMesh.vertices = vertices;
        meshFilter.sharedMesh.triangles = triangles;
        meshFilter.sharedMesh.uv = uvs;
        meshFilter.sharedMesh.RecalculateNormals();
        meshFilter.GetComponent<MeshRenderer>().sharedMaterial.SetVector("_elevation", new Vector4(min, max));
    }
}
