using UnityEngine;

public class PlatformPhysics : MonoBehaviour
{
    private Vector3 halfExtents;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        halfExtents = Vector3.Scale(mf.sharedMesh.bounds.extents, transform.localScale);
    }

    public float TopY()
    {
        return transform.position.y + halfExtents.y;
    }

    public Bounds GetAABB()
    {
        return new Bounds(transform.position, halfExtents * 2f);
    }
}
