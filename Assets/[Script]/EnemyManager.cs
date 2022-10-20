using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public float SpawnSpeed;
    [SerializeField]
    private GameObject Prefab;
    [SerializeField]
    private Transform Parent;
    [SerializeField]
    private int Level;


    private void Start()
    {
        if(Level == 1)
            InvokeRepeating("SpawnEnemy", 0.0f, SpawnSpeed);
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(Prefab, new Vector3(Random.Range(-1.88f, 1.88f), 8.0f, 0.0f), Quaternion.identity, Parent);
    }
}
