using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private bool DamageOnDestroy = false;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private Transform SpawnPoint;
    [SerializeField]
    private Transform SpawnPoint2;
    private int health = 3;
    [SerializeField]
    private SpriteRenderer Renderer;

    public ScoreManager score;

    public ScreenBounds bounds;

    private bool Gun1 = false;

    private void Start()
    {
        score = Component.FindObjectOfType<ScoreManager>();
        InvokeRepeating("FireBullet", 0.0f, FireRate);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBoundry();
    }

    private void Move()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public void CheckBoundry()
    {
        if ((transform.position.x > bounds.horizontal.max) ||
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min))
        {
            if (DamageOnDestroy)
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().UpdateHealth(-1);
            }
            Destroy(this.gameObject);
        }
    }

    void FireBullet()
    {
        if (BulletPrefab != null || SpawnPoint != null || SpawnPoint2 != null)
        {
            if (Gun1)
            {
                var bullet = GameObject.Instantiate(BulletPrefab, SpawnPoint.position, Quaternion.identity);
                Gun1 = false;
            }
            else
            {
                var bullet2 = GameObject.Instantiate(BulletPrefab, SpawnPoint2.position, Quaternion.identity);
                Gun1 = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("PlayerBullet"))
        {
            Renderer.color = Color.red;
            Destroy(collision.gameObject);
            health -= collision.gameObject.GetComponent<PlayerBullet>().Damage;
            Invoke("ResetColor", 0.1f);

            if (health <= 0)
            {
                if (Random.Range(1, 20) <= 5 && SceneManager.GetActiveScene().name != "Main")
                {
                    Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp"), transform.position, Quaternion.identity);
                }
                Destroy(this.gameObject);
                score.UpdateScore(100);
            }
        }
    }

    private void ResetColor()
    {
        Renderer.color = Color.white;
    }
}