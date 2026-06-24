using UnityEngine;

public class ExplodingBulletScript : BulletHandler
{
    void Start()
    {
        bulletType = BulletType.Exploding;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
