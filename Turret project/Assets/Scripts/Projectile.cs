using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float maxLifetime = 5f;
    public float hitDistance = 0.5f;

    private Vector3 moveDirection;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        CheckHit();
    }

    private void CheckHit()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyGO in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemyGO.transform.position);

            if (distance <= hitDistance)
            {
                Enemy enemy = enemyGO.GetComponent<Enemy>();
                
                if (enemy != null)
                {
                    enemy.TakeDamage(1); 
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
}
