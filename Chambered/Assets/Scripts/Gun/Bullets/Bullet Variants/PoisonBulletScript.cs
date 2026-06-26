using UnityEngine;

public class PoisonBulletScript : BulletHandler
{
    [Header("Bullet Data")]
    public BulletValues poisonBulletData;

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthScript otherHealth;

        otherHealth = other.GetComponent<HealthScript>();

        if (otherHealth != null && otherHealth.isDamaged)
            if (other.CompareTag("Player"))
            {
                otherHealth.health -= poisonBulletData.playerDamage;
            }
            else
            {
                otherHealth.health -= poisonBulletData.otherDamage;
            }
        
        
        StopAllCoroutines();
        DeactivateBullet();     
        return;
    }
}