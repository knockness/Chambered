using UnityEngine;

public class HealingBulletScript : BulletHandler
{
    [Header("Bullet Data")]
    public BulletValues healingBulletData;

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthScript otherHealth;

        otherHealth = other.GetComponent<HealthScript>();

        if (otherHealth != null && otherHealth.isDamaged)
            if (other.CompareTag("Player"))
            {
                otherHealth.health -= healingBulletData.playerDamage;
            }
            else
            {
                otherHealth.health -= healingBulletData.otherDamage;
            }
        

        StopAllCoroutines();
        DeactivateBullet();     
        return;
    }
}
