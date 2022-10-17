using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed;

    public Power power;

    private bool DoMove = true;
    // Start is called before the first frame update
    void Start()
    {
        SetPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoMove)
        {
            Move();
        }
    }

    private void SetPowerUp()
    {
        Power[] pwr = Resources.LoadAll<Power>("Power");
        power = pwr[Random.Range(0, pwr.Length)];
        spriteRenderer.sprite = power.sprite;
    }

    private void Move()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public void PickUp()
    {
        DoMove = false;
        transform.position = new Vector3(1.70299995f, -4.46299982f, 0.0f);
    }

}
