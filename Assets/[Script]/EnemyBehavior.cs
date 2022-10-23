using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public bool DamageOnDestroy = false;
    [SerializeField]
    private float speedY;
    [SerializeField]
    private float speedX;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private List<Transform> SpawnPoint;

    private Animator Anim;

    [SerializeField]
    private int health = 3;
    [SerializeField]
    private SpriteRenderer Renderer;

    public ScoreManager score;

    public ScreenBounds bounds;

    private bool Gun1 = false;

    private int SpawnPointIndex = 0;
    private void Start()
    {
        Anim = GetComponent<Animator>();
        score = Component.FindObjectOfType<ScoreManager>();
        InvokeRepeating("FireBullet", 0.0f, FireRate);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBoundry();
        Die();
    }

    private void Move()
    {
        if (speedX == 0)
        {
            transform.position += Vector3.down * speedY * Time.deltaTime;
        }
        else
        {
            float horizontalLength = bounds.horizontal.max - bounds.horizontal.min;
            transform.position = new Vector3(Mathf.PingPong(Time.time * speedX, horizontalLength) - bounds.horizontal.max,
                                              transform.position.y + Vector3.down.y * speedY * Time.deltaTime, 0.0f);
        }
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
        if (BulletPrefab == null)
            return;
        if ((SpawnPointIndex > SpawnPoint.Count-1))
            SpawnPointIndex = 0;
        var bullet = GameObject.Instantiate(BulletPrefab, SpawnPoint[SpawnPointIndex].position, Quaternion.identity);
        SpawnPointIndex++;

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
                if (Random.value < 0.5f && SceneManager.GetActiveScene().name != "Main")
                {
                    Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp"), transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void ResetColor()
    {
        Renderer.color = Color.white;
    }

    private void Die()
    {
        if (health <= 0)
        {
            Anim.SetBool("IsDying", true);
            if(Anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                score.UpdateScore(100);
                Destroy(this.gameObject);
            }
              
        }
    }

}