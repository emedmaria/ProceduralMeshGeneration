using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheTriangles : AbstractMeshGenerator {

    [SerializeField]
    private Vector3[] vs = new Vector3[3]; //Going from vertex 0 to 1 to 2 only works if they are positioned, so that the progression is Clockwise. 

    protected override void SetMeshNums()
	{
		numVertices = 3;
		numTriangles = 3;
	}

	protected override void SetVertices()
    {
        vertices.AddRange(vs);
    }

	/// <summary>
	/// The triangles use to join the vertices in a model
	/// </summary>
	protected override void SetTriangles()
    {
		//This means the mesh starts at Vertex 0, connects to vertex 1, then to vertex 2
		//The index of the vertices in the triangles list corresponds with the order in which the vertices are joined up
		//And the integer entry of the index is equal to the vertices index! triangles[0]=0; vertices[0]
		if(!reverseTriangles)
		{
			triangles.Add(0);
			triangles.Add(1);
			triangles.Add(2);
		}
		else
		{
			//Changes the order how the Triangles are in (anti-clockwise) - BackFace Culling
			triangles.Add(0);
			triangles.Add(2);
			triangles.Add(1);
		}
    }

	protected override void SetNormals(){}
	protected override void SetTangents(){}
	protected override void SetUVs(){}
	protected override void SetVertecColours(){}
}
