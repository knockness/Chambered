using UnityEngine;

public class ExplodingBulletScript : BulletHandler
{
    [Header("Bullet Data")]
    public BulletValues explodingBulletData;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        HealthScript otherHealth;

        otherHealth = other.GetComponent<HealthScript>();

        if (otherHealth != null && otherHealth.isDamaged)
            if (other.CompareTag("Player"))
            {
                otherHealth.health -= explodingBulletData.playerDamage;
            }
            else
            {
                otherHealth.health -= explodingBulletData.otherDamage;
            }
        

        StopAllCoroutines();
        DeactivateBullet();      
        return;
    }
}
