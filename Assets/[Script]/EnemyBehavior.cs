using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
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

    public ScreenBounds bounds;

    private bool Gun1 = false;

    private void Start()
    {
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
            Destroy(this.gameObject);
        }
    }

    void FireBullet()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("PlayerBullet"))
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
            Destroy(collision.gameObject);
            health--;
            Invoke("ResetColor", 0.1f);
            if (health <= 0)
            {
                if (Random.Range(1,20) <= 5)
                {
                    Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp"), transform.position, Quaternion.identity);
                }
                Destroy(this.gameObject);
            }

        }
    }

    private void ResetColor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
    }
}
