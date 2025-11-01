using UnityEngine;
using System.Collections;

public class SpawnerCore : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float interval;
    public float randomVariance;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            int lane = Random.Range(0, 3);
            GameObject go = Instantiate(obstaclePrefab);
            var ob = go.GetComponent<ObstacleCore>();
            ob.lane = lane;
            yield return new WaitForSeconds(interval + Random.Range(-randomVariance, randomVariance));
        }
    }
}
