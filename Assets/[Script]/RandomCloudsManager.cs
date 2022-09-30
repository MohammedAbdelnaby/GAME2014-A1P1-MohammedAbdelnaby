using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCloudsManager : MonoBehaviour
{
    public List<GameObject> RandomObjects;

    public Boundry SpawnTimeRange;

    public Boundry RandomSpawnPoint;

    public float time;
    private void Start()
    {
        ResetTime();
    }
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0.0f)
        {
            SpawnCloud();
            ResetTime();
        }
    }

    public void SpawnCloud()
    {
        GameObject.Instantiate(RandomObjects[Random.Range(0, RandomObjects.Count - 1)], 
            new Vector3(Random.Range(RandomSpawnPoint.min, RandomSpawnPoint.max),
            transform.position.y, 
            transform.position.z), 
            Quaternion.identity);
    }

    public void ResetTime()
    {
        time = Random.Range(SpawnTimeRange.min, SpawnTimeRange.max);
    }
}
