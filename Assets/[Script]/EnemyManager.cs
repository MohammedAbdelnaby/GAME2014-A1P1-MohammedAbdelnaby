using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public List<GameObject> enemies;
    public float SpawnSpeed;
    [SerializeField]
    private GameObject Prefab;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        enemies = new List<GameObject>();
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0.0f, SpawnSpeed);
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(Prefab, new Vector3(Random.Range(-1.88f, 1.88f), 8.0f, 0.0f), Quaternion.identity, transform.parent);
    }
}
