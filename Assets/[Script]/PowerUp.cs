using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed;

    public Boundry boundry;

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
        CheckBoundry();
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

    public void CheckBoundry()
    {
        if ((transform.position.y > boundry.max) ||
            (transform.position.y < boundry.min))
        {
            Destroy(this.gameObject);
        }
    }

    public void PickUp()
    {
        DoMove = false;
        if (power.pwr == PowerTypes.HEALTH)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.position = new Vector3(1.70299995f, -4.46299982f, 0.0f);
        spriteRenderer.sortingLayerName = "InSlot";
        GameObject InSlot = GameObject.Find("PowerPicked");
        if (InSlot != null)
        {
            Destroy(InSlot);
        }
        name = "PowerPicked";
    }
}
