using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public GameObject[] noGoZones;
    public GameObject finishZone;
    public GameObject winUI;

    public float dangerDistance = 1.5f;
    public float deathDistance = 0.6f;
    public float shakeAmount = 0.05f;

    private bool gameWon = false;

    void Update()
    {
        if (gameWon) return;

        HandleMovement();
        CheckNoGoZones();
        CheckFinishZone();
    }

    void HandleMovement()
    {
        float moveX = 0;
        float moveY = 0;

        if (Input.GetKey(KeyCode.W)) moveY = 1;
        if (Input.GetKey(KeyCode.S)) moveY = -1;
        if (Input.GetKey(KeyCode.A)) moveX = -1;
        if (Input.GetKey(KeyCode.D)) moveX = 1;

        float deltaX = moveX * moveSpeed * Time.deltaTime;
        float deltaY = moveY * moveSpeed * Time.deltaTime;

        transform.position = new Vector3(
            transform.position.x + deltaX,
            transform.position.y + deltaY,
            transform.position.z
        );
    }

    void CheckNoGoZones()
    {
        foreach (GameObject zone in noGoZones)
        {
            if (zone == null) continue;

            float dx = transform.position.x - zone.transform.position.x;
            float dy = transform.position.y - zone.transform.position.y;
            float distance = Mathf.Sqrt(dx * dx + dy * dy);

            SpriteRenderer sr = zone.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (distance < dangerDistance)
                {
                    sr.color = Color.red;

                    zone.transform.position = new Vector3(
                        zone.transform.position.x + Random.Range(-shakeAmount, shakeAmount),
                        zone.transform.position.y + Random.Range(-shakeAmount, shakeAmount),
                        zone.transform.position.z
                    );
                }
                else
                {
                    sr.color = Color.gray;
                }
            }

            if (distance < deathDistance)
            {
                RestartScene();
            }
        }
    }

    void CheckFinishZone()
    {
        if (finishZone == null || winUI == null) return;

        float dx = transform.position.x - finishZone.transform.position.x;
        float dy = transform.position.y - finishZone.transform.position.y;
        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        if (distance < 1f)
        {
            winUI.SetActive(true);
            gameWon = true;
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
