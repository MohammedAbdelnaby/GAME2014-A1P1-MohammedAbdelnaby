using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ship : MonoBehaviour
{
    protected int Health;
    protected float SpeedX;
    protected float SpeedY;
    protected float FireRate;
    protected int Damage;
    protected SpriteRenderer Sprite;
    public void Initiate(int hp, float SX, float SY, float FR, int DMG, SpriteRenderer SR)
    {
        Health = hp;
        SpeedX = SX;
        SpeedY = SY;
        FireRate = FR;
        Damage = DMG;
        Sprite = SR;
    }

    public virtual void Move() { }
    public virtual void FireBullet() { }
    public virtual void CheckBounds() { }

    public virtual void UpdateHealth(int dmg) 
    {
        Health -= dmg;
        Sprite.color = Color.red;
        Invoke("ResetColor", 0.1f);
        if (Health <= 0)
        {
            Destroy(base.gameObject);
        }
    }
    public virtual float GetHealth() { return Health; }
    public virtual Vector2 GetSpeed() { return new Vector2(SpeedX, SpeedY); }
    public virtual float GetFireRate() { return FireRate; }
    public virtual float GetDamage() { return Damage; }

    public void ResetColor()
    {
        Sprite.color = Color.white;
    }
}
