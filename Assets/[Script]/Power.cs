using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPower", menuName = "Items/New Power")]
public class Power : ScriptableObject
{

    [SerializeField]
    public PowerTypes pwr;

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public float strength;
    [SerializeField]
    public float fireRate;
    [SerializeField]
    public float health;
    [SerializeField]
    public float speed;
    [SerializeField]
    public bool moreGuns;

}
