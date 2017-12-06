using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To render the mesh we also need a MeshFilter -is a container which passes the meshes information to the Renderer - and a MeshRender - renders the mesh-
/// </summary>
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
/// <summary>
/// This will call the Update method whenever something changes in the scene.
/// </summary>
[ExecuteInEditMode] 
public class AllTheTriangles : MonoBehaviour {

    [SerializeField] private Material material;
    [SerializeField] private Vector3 []  vs = new Vector3[3]; //Going from vertex 0 to 1 to 2 only works if they are positioned, so that the progression is Clockwise. 
    private List<Vector3> vertices;
    private List<int> triangles; 


    /// <summary>
    /// Stores the mesh information (vertices, triangles, normals, etc. )
    /// </summary>
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = this.GetComponent<MeshFilter>();
        meshRenderer = this.GetComponent<MeshRenderer>();

    }

    private void Update()
    {
        //MeshRenderer needs a material to display the mesh
        meshRenderer.material = material;

        //initialise Vertex and Triangles
        vertices = new List<Vector3>();
        triangles = new List<int>();

        //Create the Mesh
        CreateMesh();
    }

    private void CreateMesh()
    {
        mesh = new Mesh();
        SetVertices();
        SetTriangles();

        ///This should aleays be done in this order (Vertices first, Triangles second)
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);  // 0 we use a single material

        //Set the meshfilter to be the mesh that just created
        //meshFilter.mesh = mesh;  //.mesh uses a copy rather .sharedMesh
        meshFilter.sharedMesh = mesh; 

    }

    private void SetVertices()
    {
        vertices.AddRange(vs);
    }

    /// <summary>
    /// The triangles use to join the vertices in a model
    /// </summary>
    private void SetTriangles()
    {
        //This means the mesh starts at Vertex 0, connects to vertex 1, then to vertex 2
        //The index of the vertices in the triangles list corresponds with the order in which the vertices are joined up
        //And the integer entry of the index is equal to the vertices index! triangles[0]=0; vertices[0]
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

    }
}
