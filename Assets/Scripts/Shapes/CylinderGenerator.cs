using UnityEngine;

public class CylinderGenerator : IShapeGenerator
{
    private GameObject currentShape;
    private int segments = 20; // more than 5 segments for smoothness

    public GameObject Generate(Vector3 position)
    {
        Erase();

        currentShape = new GameObject("Cylinder");
        MeshFilter mf = currentShape.AddComponent<MeshFilter>();
        MeshRenderer mr = currentShape.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        Mesh mesh = new Mesh();

        // Cylinder parameters
        float height = 2f;
        float radius = 1f;

        Vector3[] vertices = new Vector3[(segments + 1) * 2 + segments * 2];
        int vert = 0;

        // Top center and bottom center vertices
        vertices[vert++] = new Vector3(0, height / 2f, 0);
        vertices[vert++] = new Vector3(0, -height / 2f, 0);

        // Side vertices top and bottom
        for (int i = 0; i <= segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[vert++] = new Vector3(x, height / 2f, z);    // top circle
            vertices[vert++] = new Vector3(x, -height / 2f, z);   // bottom circle
        }

        // Triangles
        int trianglesCount = segments * 12; // 3 per triangle, 4 triangles per segment
        int[] triangles = new int[trianglesCount];
        int tri = 0;

        // Top circle triangles
        for (int i = 0; i < segments; i++)
        {
            triangles[tri++] = 0;            // top center
            triangles[tri++] = 2 + i * 2;    // current top perimeter
            triangles[tri++] = 2 + ((i + 1) * 2) % ((segments + 1) * 2);
        }

        // Bottom circle triangles
        int bottomCenterIndex = 1;
        for (int i = 0; i < segments; i++)
        {
            triangles[tri++] = bottomCenterIndex;
            triangles[tri++] = 3 + ((i + 1) * 2) % ((segments + 1) * 2);
            triangles[tri++] = 3 + i * 2;
        }

        // Side triangles (two per segment)
        for (int i = 0; i < segments; i++)
        {
            int topCurrent = 2 + i * 2;
            int topNext = 2 + ((i + 1) * 2) % ((segments + 1) * 2);
            int bottomCurrent = topCurrent + 1;
            int bottomNext = topNext + 1;

            // First triangle
            triangles[tri++] = topCurrent;
            triangles[tri++] = bottomCurrent;
            triangles[tri++] = topNext;

            // Second triangle
            triangles[tri++] = topNext;
            triangles[tri++] = bottomCurrent;
            triangles[tri++] = bottomNext;
        }

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
