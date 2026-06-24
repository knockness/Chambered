using UnityEngine;

public class HealingBulletScript : BulletHandler
{
    void Start()
    {
        bulletType = BulletType.Healing;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
