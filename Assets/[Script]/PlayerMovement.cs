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


    private float YPostion = -3.0f;
    public Boundry boundry;
    public bool IsMobileInput;
    private Camera camera;

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
    }
}
