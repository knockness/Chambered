using UnityEngine;

public class PoisonBulletScript : BulletHandler
{
    void Start()
    {
        bulletType = BulletType.Poison;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}