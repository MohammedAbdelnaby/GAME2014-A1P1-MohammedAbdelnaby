using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float FireRate;
    private int health = 3;
    [SerializeField]
    private SpriteRenderer Renderer;
    [SerializeField]
    private Power power = null;

    private float YPostion = -3.0f;
    public Boundry boundry;
    public bool IsMobileInput;
    private Camera camera;
    private int Damage = 1;


    private void Start()
    {
        camera = Camera.main;
        IsMobileInput = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.Android;
        InvokeRepeating("FireBullet", 0.0f, FireRate);
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
        var bullet = GameObject.Instantiate(BulletPrefab, SpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().Damage = Damage;
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
        switch (power.pwr)
        {
            case PowerTypes.FIRERATE:
                CancelInvoke("FireBullet");
                FireRate -= power.value;
                InvokeRepeating("FireBullet", 0.0f, FireRate);
                Debug.Log("FireRate");
                break;
            case PowerTypes.STRENGTH:
                Damage += (int)power.value;
                Debug.Log("Strength");
                break;
            case PowerTypes.MOREGUNS:
                Debug.Log("MoreGuns");
                break;
            case PowerTypes.SPEED:
                speed += power.value;
                Debug.Log("Speed");
                break;
            case PowerTypes.HEALTH:
                if (health != 3)
                {
                    health += (int)power.value;
                }
                Debug.Log("Health");
                break;
            case PowerTypes.NONE:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            power = collision.gameObject.GetComponent<PowerUp>().power;
            if (power != null)
            {
                HasPower();
            }
            collision.gameObject.GetComponent<PowerUp>().PickUp();
        }
    }
}
