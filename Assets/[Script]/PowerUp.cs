using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        SetPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void SetPowerUp()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    private void Move()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
