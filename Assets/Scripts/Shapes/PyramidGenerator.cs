using UnityEngine;

public class PyramidGenerator : IShapeGenerator
{
    private GameObject currentShape;

    public GameObject Generate(Vector3 position)
    {
        Erase();

        currentShape = new GameObject("Pyramid");
        MeshFilter mf = currentShape.AddComponent<MeshFilter>();
        MeshRenderer mr = currentShape.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        Mesh mesh = new Mesh();

        // Pyramid vertices (square base + apex)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1, 0, -1), // base 0
            new Vector3(1, 0, -1),  // base 1
            new Vector3(1, 0, 1),   // base 2
            new Vector3(-1, 0, 1),  // base 3
            new Vector3(0, 1.5f, 0) // apex 4
        };

        // Triangles: 2 for base, 4 for sides
        int[] triangles = new int[]
        {
            0, 1, 2, 0, 2, 3,        // base
            0, 1, 4,                 // side 1
            1, 2, 4,                 // side 2
            2, 3, 4,                 // side 3
            3, 0, 4                  // side 4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        mf.mesh = mesh;
        currentShape.transform.position = position;

        return currentShape;
    }

    public void Erase()
    {
        if (currentShape != null)
            GameObject.Destroy(currentShape);
    }
}
