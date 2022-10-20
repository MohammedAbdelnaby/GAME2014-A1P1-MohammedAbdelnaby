using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    public BulletDirection BulletDirection;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int Damage = 1;

    public ScreenBounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        SetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBoundry();
    }
    public void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
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

    public void SetDirection()
    {
        switch (BulletDirection)
        {
            case BulletDirection.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case BulletDirection.UpRight:
                transform.rotation = Quaternion.Euler(0, 0, 45);
                break;
            case BulletDirection.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case BulletDirection.RightDown:
                transform.rotation = Quaternion.Euler(0, 0, 135);
                break;
            case BulletDirection.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case BulletDirection.DownLeft:
                transform.rotation = Quaternion.Euler(0, 0, 225);
                break;
            case BulletDirection.Left:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case BulletDirection.LeftUp:
                transform.rotation = Quaternion.Euler(0, 0, 315);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().UpdateHealth(-Damage);
            Destroy(this.gameObject);
        }
    }
}
