using UnityEngine;

public class ObstacleCore : MonoBehaviour
{
    public int lane;
    public float speed;
    public float hitRangeY;  // Vertical hit tolerance
    public float hitRangeX;  // Horizontal hit tolerance
    public float destroyY;   // Offscreen cleanup
    public int damage;

    private Vector3 start;
    private Vector3 end;
    private bool hit = false;
    private PlayerCore player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerCore>();
        if (player == null)
        {
            Debug.LogWarning("No PlayerCore found in scene!");
            return;
        }

        float z = player.spawnZ;

        // Starting and ending positions for each lane
        switch (lane)
        {
            case 0: start = new Vector3(-3.5f, 25f, z); end = new Vector3(-7f, -20f, z); break;
            case 1: start = new Vector3(0f, 25f, z); end = new Vector3(0f, -20f, z); break;
            case 2: start = new Vector3(3.5f, 25f, z); end = new Vector3(7f, -20f, z); break;
        }

        transform.position = start;
    }

    private void Update()
    {
        if (player == null || GameCore.main == null || GameCore.main.IsGameOver) return;

        // Move obstacle downward along its lane path
        Vector3 dir = (end - start).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // --- Collision Check ---
        if (!hit)
        {
            float dy = Mathf.Abs(transform.position.y - player.transform.position.y);
            float dx = Mathf.Abs(transform.position.x - player.transform.position.x);

            // Player is hit only if close enough horizontally *and* vertically AND near ground
            bool inSameLane = dx <= hitRangeX;
            bool closeVertically = dy <= hitRangeY;
            bool playerIsGrounded = Mathf.Approximately(player.transform.position.y, player.groundY);

            if (inSameLane && closeVertically && playerIsGrounded)
            {
                player.TakeDamage(damage);
                hit = true;
                Destroy(gameObject, 0.05f);
                return;
            }
        }

        // Destroy if offscreen
        if (transform.position.y <= destroyY)
            Destroy(gameObject);
    }
}
