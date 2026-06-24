using UnityEngine;

public class NormalBulletScript : BulletHandler
{
    void Start()
    {
        bulletType = BulletType.Normal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthScript otherHealth;

            otherHealth = other.GetComponent<HealthScript>();

            if (otherHealth.isDamaged)
                //otherHealth.health -= baseDamage;
                
            return;
    }
}
