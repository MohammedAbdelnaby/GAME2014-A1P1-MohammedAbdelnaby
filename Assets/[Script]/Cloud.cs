using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Boundry boundry;
    // Update is called once per frame
    void Update()
    {
        Move();
        OutOfBounds();
    }

    public void Move()
    {
        transform.position -= new Vector3(0.0f, Speed.speed * Time.deltaTime, 0.0f);
    }

    public void OutOfBounds()
    {
        if (transform.position.y < boundry.min)
        {
            Destroy(gameObject);
        }
    }
}
