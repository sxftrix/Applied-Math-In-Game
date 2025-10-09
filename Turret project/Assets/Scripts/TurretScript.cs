using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TurretScript : MonoBehaviour
{
    public Transform firePoint;
    private Transform targetTransform;
    public GameObject projectilePrefab;

    public float rotationSpeed = 180f;
    public float fireRange = 10f;
    public float alignmentAngleThreshold = 10f;
    public float fireCooldown = 1.5f;
    public float muzzleOffset = 0.3f;

    private float nextFireTime = 0f;

    void Update()
    {
        FindTarget();

        if (targetTransform == null) return;

        Vector2 dirToTarget = targetTransform.position - transform.position;
        float distance = dirToTarget.magnitude;

        float targetAngle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);

        if (Time.time < nextFireTime)
            return;

        if (distance <= fireRange)
        {
            float angleDiff = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle));
            if (angleDiff <= alignmentAngleThreshold)
            {
                FireProjectile();
                nextFireTime = Time.time + fireCooldown;
            }
        }
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        float closestDistanceSqr = Mathf.Infinity;
        Transform bestTarget = null;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToTarget = enemy.transform.position - transform.position;
            float distanceSqr = directionToTarget.sqrMagnitude;

            if (distanceSqr < fireRange * fireRange && distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                bestTarget = enemy.transform;
            }
        }

        targetTransform = bestTarget;
    }

    private void FireProjectile()
    {
        if (firePoint == null)
        {
            Debug.LogError("Fire Point not set on TurretController!");
            return;
        }

        Vector3 spawnPos = firePoint.position + firePoint.up * muzzleOffset;
        spawnPos.z = 0f;

        GameObject projectileGO = Instantiate(projectilePrefab, spawnPos, firePoint.rotation);

        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDirection(firePoint.up); // or firePoint.right if your turret fires sideways
        }
    }
}
