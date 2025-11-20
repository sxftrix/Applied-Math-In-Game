using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ShapeRotater : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 30f; // degrees per second

    private MeshFilter meshFilter;
    private Mesh originalMesh;
    private float spinAngle = 0f;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter.sharedMesh == null)
            meshFilter.sharedMesh = new Mesh();

        originalMesh = Instantiate(meshFilter.sharedMesh);
    }

    private void Update()
    {
        if (originalMesh == null || originalMesh.vertexCount == 0)
            return;

        spinAngle += rotationSpeed * Time.deltaTime;

        Matrix4x4 rotationMatrix = GLRotation.RotateY(spinAngle);

        ApplyRotation(rotationMatrix);
    }

    private void ApplyRotation(Matrix4x4 rotationMatrix)
    {
        Vector3[] baseVerts = originalMesh.vertices;
        Vector3[] rotatedVerts = new Vector3[baseVerts.Length];

        for (int i = 0; i < baseVerts.Length; i++)
            rotatedVerts[i] = rotationMatrix.MultiplyPoint3x4(baseVerts[i]);

        Mesh mesh = meshFilter.sharedMesh;
        mesh.vertices = rotatedVerts;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    public static class GLRotation
    {
        public static Matrix4x4 RotateX(float degrees)
        {
            // r = rad c = cos s = sin
            float r = degrees * Mathf.Deg2Rad;
            float c = Mathf.Cos(r);
            float s = Mathf.Sin(r);
            return new Matrix4x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, c, -s, 0),
                new Vector4(0, s, c, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4x4 RotateY(float degrees)
        {
            float r = degrees * Mathf.Deg2Rad;
            float c = Mathf.Cos(r);
            float s = Mathf.Sin(r);
            return new Matrix4x4(
                new Vector4(c, 0, s, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(-s, 0, c, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4x4 RotateZ(float degrees)
        {
            float r = degrees * Mathf.Deg2Rad;
            float c = Mathf.Cos(r);
            float s = Mathf.Sin(r);
            return new Matrix4x4(
                new Vector4(c, -s, 0, 0),
                new Vector4(s, c, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
        }
    }
}