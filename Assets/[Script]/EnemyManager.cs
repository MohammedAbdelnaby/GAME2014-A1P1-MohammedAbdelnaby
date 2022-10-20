using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public float SpawnSpeed;
    [SerializeField]
    private GameObject BasicPrefab;
    [SerializeField]
    private GameObject BlockPrefab;
    [SerializeField]
    private Transform Parent;
    [SerializeField]
    private int Level;
    [SerializeField]
    private ScoreManager score;

    private void Start()
    {
        if (Level == 1)
            InvokeRepeating("SpawnEnemy", 0.0f, SpawnSpeed);
        if (Level == 2)
        {
            InvokeRepeating("SpawnEnemy", 0.0f, SpawnSpeed);
            InvokeRepeating("SpawnBlockEnemy", 0.0f, SpawnSpeed + 5.0f);
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(BasicPrefab, new Vector3(Random.Range(-1.88f, 1.88f), 8.0f, 0.0f), Quaternion.identity, Parent);
    }
    private void SpawnBlockEnemy()
    {
        if (Random.value < 0.5f)
        {
            var enemy = Instantiate(BlockPrefab, new Vector3(-1.20f, 8.0f, 0.0f), Quaternion.identity, Parent);
        }
        if (Random.value > 0.5f)
        {
            var enemy = Instantiate(BlockPrefab, new Vector3(1.20f, 8.0f, 0.0f), Quaternion.identity, Parent);
        }
    }
}
