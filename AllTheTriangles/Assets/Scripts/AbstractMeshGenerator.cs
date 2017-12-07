using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To render the mesh we also need a MeshFilter -is a container which passes the meshes information to the Renderer - and a MeshRender - renders the mesh-
/// </summary>
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshCollider))]
/// <summary>
/// This will call the Update method whenever something changes in the scene.
/// </summary>
[ExecuteInEditMode]
public abstract class AbstractMeshGenerator : MonoBehaviour
{

    [SerializeField]
    protected Material material;
    //[SerializeField]
   // protected Vector3[] vs = new Vector3[3]; //Going from vertex 0 to 1 to 2 only works if they are positioned, so that the progression is Clockwise. 
    [SerializeField]
    protected bool reverseTriangles = false;
    protected List<Vector3> vertices;
    protected List<int> triangles;
    protected List<Vector3> normals;
    protected List<Vector4> tangents;
    protected List<Vector2> uvs;
    protected List<Color32> vertexColors;

    protected int numVertices;
    protected int numTriangles;


    /// <summary>
    /// Stores the mesh information (vertices, triangles, normals, etc. )
    /// </summary>
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private void Awake()
    {
        meshFilter = this.GetComponent<MeshFilter>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        meshCollider = this.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        //MeshRenderer needs a material to display the mesh
        meshRenderer.material = material;

        //initialise Vertex and Triangles
        InitMesh();
        SetMeshNums();

        //Create the Mesh
        CreateMesh();
    }

    private void InitMesh()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        normals = new List<Vector3>();
        tangents = new List<Vector4>();
        uvs = new List<Vector2>();
        vertexColors = new List<Color32>();
    }

    private void CreateMesh()
    {
        mesh = new Mesh();
        SetVertices();
        SetTriangles();

        SetNormals();
        SetTangents();
        SetUVs();
        SetVertecColours();

        if (ValidateMesh())
        {
            ///This should always be done in this order (Vertices first, Triangles second)
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);  // 0 we use a single material

            if (normals.Count == 0)
            {
                mesh.RecalculateNormals();
                normals.AddRange(mesh.normals);
            }

            mesh.SetNormals(normals);
            mesh.SetTangents(tangents);
            mesh.SetUVs(0, uvs);
            mesh.SetColors(vertexColors);

            //Set the meshfilter to be the mesh that just created
            //meshFilter.mesh = mesh;  //.mesh uses a copy rather .sharedMesh
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
        }
    }

    private bool ValidateMesh()
    {
        string errorStr = string.Empty;

        errorStr += numTriangles == triangles.Count ? string.Empty : "Should be " + numTriangles + "triangles, but there are " + triangles.Count;
        errorStr += numVertices == vertices.Count ? string.Empty : "Should be " + numVertices + "vertices, but there are " + vertices.Count;

        //Check there are correct number of normals - there should be the same number of normals are there are vertices. If we're not manually calculating normals, there will be 0. 
        //Similarly for tangents, ucs, vertexColors
        errorStr += (numVertices == normals.Count || normals.Count == 0) ? string.Empty : "Should be " + numVertices + "normals, but there are " + normals.Count;
        errorStr += (numVertices == tangents.Count || tangents.Count == 0) ? string.Empty : "Should be " + numVertices + "tangents, but there are " + tangents.Count;
        errorStr += (numVertices == uvs.Count || uvs.Count == 0) ? string.Empty : "Should be " + numVertices + "uvs, but there are " + uvs.Count;
        errorStr += (numVertices == vertexColors.Count || vertexColors.Count == 0) ? string.Empty : "Should be " + vertexColors + "vertexColors, but there are " + vertexColors.Count;

        bool isValid = string.IsNullOrEmpty(errorStr);
        if (!isValid)
        {
            Debug.LogError("Not drawing mesh. " + errorStr);
        }

        return isValid;
    }

    protected abstract void SetMeshNums();
    protected abstract void SetVertices();
    protected abstract void SetTriangles();
    protected abstract void SetNormals();
    protected abstract void SetTangents();
    protected abstract void SetUVs();
    protected abstract void SetVertecColours();

}
