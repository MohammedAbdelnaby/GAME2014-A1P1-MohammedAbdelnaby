using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private Transform SpawnPoint;
    [SerializeField]
    private Transform Gun1;
    [SerializeField]
    private Transform Gun2;
    [SerializeField]
    private float FireRate;
    private int health = 3;
    [SerializeField]
    private Power power;
    [SerializeField]
    private SpriteRenderer Renderer;
    [SerializeField]
    private List<SpriteRenderer> HealthRenderer;

    private float YPostion = -3.0f;
    public Boundry boundry;
    public bool IsMobileInput;
    private Camera camera;
    private int Damage = 1;
    private bool moreGuns = false;
    private float FireRateTime;


    private void Start()
    {
        Renderer = this.GetComponent<SpriteRenderer>();
        camera = Camera.main;
        IsMobileInput = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.Android;
        FireRateTime = FireRate;
    }

    void Update()
    {
        if (IsMobileInput)
        {
            MobileInput();
        }
        else
        {
            KeyboardInput();
        }
        Move();
        FireBullet();
    }

    public void UpdateHealth(int value)
    {
        health += value;
        switch (health)
        {
            case -1:
                SceneManager.LoadScene("GameOver");
                break;
            case 0:
                Renderer.color = Color.red;
                HealthRenderer[0].enabled = false;
                HealthRenderer[1].enabled = false;
                HealthRenderer[2].enabled = false;
                break;
            case 1:
                Renderer.color = Color.white;
                HealthRenderer[0].enabled = false;
                HealthRenderer[1].enabled = false;
                HealthRenderer[2].enabled = true;
                break;
            case 2:
                Renderer.color = Color.white;
                HealthRenderer[0].enabled = false;
                HealthRenderer[1].enabled = true;
                HealthRenderer[2].enabled = true;
                break;
            case 3:
                Renderer.color = Color.white;
                HealthRenderer[0].enabled = true;
                HealthRenderer[1].enabled = true;
                HealthRenderer[2].enabled = true;
                break;
            default:
                if (health > 4)
                {
                    health = 3;
                }
                else if(health < 0)
                {
                    health = 0;
                }
                break;
        }
    }

    public void Move()
    {
        var clampedPostion = Mathf.Clamp(transform.position.x, boundry.min, boundry.max);
        transform.position = new Vector2(clampedPostion, YPostion);
    }

    public void KeyboardInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        transform.position += new Vector3(x, 0.0f, 0.0f);
    }

    public void MobileInput()
    {
        foreach (var touch in Input.touches)
        {
            var distination = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, distination, Time.deltaTime * speed);
        }
    }

    void FireBullet()
    {
        FireRateTime -= Time.deltaTime;
        if (FireRateTime <= 0.0f && !moreGuns)
        {
            var bullet = GameObject.Instantiate(BulletPrefab, SpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().Damage = Damage;
            FireRateTime = FireRate;
        }
        else if(FireRateTime <= 0.0f && moreGuns)
        {
            var bullet1 = GameObject.Instantiate(BulletPrefab, Gun1.position, Quaternion.identity);
            bullet1.GetComponent<PlayerBullet>().Damage = Damage;
            var bullet2 = GameObject.Instantiate(BulletPrefab, Gun2.position, Quaternion.identity);
            bullet2.GetComponent<PlayerBullet>().Damage = Damage;
            FireRateTime = FireRate;
        }
    }

    public void SetPower(Power pwr)
    {
        pwr = power;
    }

    public Power GetPower()
    {
        return power;
    }

    private void HasPower()
    {
        if (power.pwr != PowerTypes.HEALTH)
        {
            Damage = 1;
            FireRate = 0.4f;
            moreGuns = false;
            speed = 5.0f;
        }
        switch (power.pwr)
        {
            case PowerTypes.FIRERATE:
                FireRate -= power.fireRate;
                return;
            case PowerTypes.STRENGTH:
                Damage += (int)power.strength;
                Debug.Log("Strength");
                return;
            case PowerTypes.MOREGUNS:
                moreGuns = true;
                Debug.Log("MoreGuns");
                return;
            case PowerTypes.SPEED:
                speed += power.speed;
                Debug.Log("Speed");
                return;
            case PowerTypes.HEALTH:
                if (health != 3)
                {
                    UpdateHealth((int)power.health);
                }
                Debug.Log("Health");
                return;
            case PowerTypes.NONE:
                return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            PowerUp tempPower = collision.gameObject.GetComponent<PowerUp>();
            power = tempPower.power;
            if (power != null)
            {
                HasPower();
                tempPower.PickUp();
            }
        }
    }
}
