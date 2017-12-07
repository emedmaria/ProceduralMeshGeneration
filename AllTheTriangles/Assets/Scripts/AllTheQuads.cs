using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheQuads : AbstractMeshGenerator {

    [SerializeField]
    private Vector3[] vs = new Vector3[4]; //Going from vertex 0 to 1 to 2 only works if they are positioned, so that the progression is Clockwise. 

    protected override void SetMeshNums() {
        numVertices = 4;
        //There are two geometric triangles which need three ints each to define them 
        numTriangles = 6;
    }
    protected override void SetVertices() {
        vertices.AddRange(vs);
    }
    protected override void SetTriangles() {

        //First Triangle
        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(2);

        //Second Triangle
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(3);
    }

    protected override void SetNormals() { }
    protected override void SetTangents() { }
    protected override void SetUVs() { }
    protected override void SetVertecColours() { }
}
