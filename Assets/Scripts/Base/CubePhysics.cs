using UnityEngine;

public class CubePhysics : MonoBehaviour
{
    public float gravity = -12f;
    public float jumpForce = 6f;

    private float verticalVelocity = 0f;

    [SerializeField]
    private bool isGrounded = false;

    private MeshRenderer meshRenderer;
    private Color originalColor;

    private Vector3 halfExtents;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;

        MeshFilter mf = GetComponent<MeshFilter>();
        // Scale extents correctly per axis
        halfExtents = Vector3.Scale(mf.sharedMesh.bounds.extents, transform.localScale);
    }

    void Update()
    {
        HandleJump();    // Process jump input first
        ApplyGravity();  // Then apply gravity
        CheckCollision(); // Finally check collision
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump triggered!");
            verticalVelocity = jumpForce;
            isGrounded = false;
        }
    }

    void ApplyGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y += verticalVelocity * Time.deltaTime;
        transform.position = pos;
    }

    void CheckCollision()
    {
        bool collided = false;

        PlatformPhysics[] platforms = Object.FindObjectsByType<PlatformPhysics>(FindObjectsSortMode.None);

        foreach (var platform in platforms)
        {
            if (IsOverlapping(platform))
            {
                float platformTop = platform.TopY();
                Vector3 pos = transform.position;

                // Only snap and reset velocity if falling or close to platform
                if (pos.y <= platformTop + halfExtents.y + 0.01f && verticalVelocity <= 0f)
                {
                    pos.y = platformTop + halfExtents.y;
                    transform.position = pos;

                    verticalVelocity = 0f;
                    isGrounded = true;

                    collided = true;
                    break;
                }
            }
        }

        meshRenderer.material.color = collided ? Color.green : originalColor;
        if (!collided)
            isGrounded = false;
    }

    bool IsOverlapping(PlatformPhysics platform)
    {
        Bounds cubeBounds = GetAABB();
        Bounds platformBounds = platform.GetAABB();

        return cubeBounds.min.x <= platformBounds.max.x &&
               cubeBounds.max.x >= platformBounds.min.x &&
               cubeBounds.min.y <= platformBounds.max.y &&
               cubeBounds.max.y >= platformBounds.min.y &&
               cubeBounds.min.z <= platformBounds.max.z &&
               cubeBounds.max.z >= platformBounds.min.z;
    }

    Bounds GetAABB()
    {
        return new Bounds(transform.position, halfExtents * 2f);
    }
}
