using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    
    public Boundry boundry;
    void Update()
    {
        Move();
        BackgroundReset();
    }

    public void Move()
    {
        transform.position -= new Vector3(0.0f, Time.deltaTime * Speed.speed, 0.0f);
    }

    public void BackgroundReset()
    {
        if (transform.position.y < boundry.min)
        {
            transform.position = new Vector3(0.0f, boundry.max, 0.0f);
        }
    }
}
