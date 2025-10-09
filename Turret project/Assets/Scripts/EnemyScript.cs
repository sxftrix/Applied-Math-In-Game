using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;
    
    public float moveSpeed = 3f;
    private int currentPathIndex = 0;
    public Image healthBarFill;
    
    private Vector3[] pathPoints = new Vector3[]
    {
        new Vector3(-11.75f, -0.56f, 0f),
        new Vector3(-8.56f, -0.56f, 0f),
        new Vector3(-8.56f, -3.23f, 0f),
        new Vector3(8.51f, -3.23f, 0f),
        new Vector3(8.51f, -0.52f, 0f),
        new Vector3(11.74f, -0.52f, 0f)
    };

    void Awake()
    {
        currentHealth = maxHealth;
        transform.position = pathPoints[0];
        gameObject.tag = "Enemy";
    }

    void Update()
    {
        FollowPath();
        // Update health bar UI
        if (healthBarFill != null)
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    private void FollowPath()
    {
        if (currentPathIndex >= pathPoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = pathPoints[currentPathIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPathIndex++;
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth == 1)
        {
            GetComponent<Renderer>().material.color = Color.yellow; 
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
