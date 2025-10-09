using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    public float spawnInterval = 3f;
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity); 
        }
        else
        {
            Debug.LogError("Enemy Prefab not set on Enemy Spawner!");
        }
    }
}
