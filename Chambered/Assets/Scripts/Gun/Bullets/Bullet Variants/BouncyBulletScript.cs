using UnityEngine;

public class BouncyBulletScript : BulletHandler
{
    void Start()
    {
        bulletType = BulletType.Bouncy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
