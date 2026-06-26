using UnityEngine;

public class NormalBulletScript : BulletHandler
{
    [Header("Bullet Data")]
    public BulletValues normalBulletData;

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthScript otherHealth;

        otherHealth = other.GetComponent<HealthScript>();

        if (otherHealth != null && otherHealth.isDamaged)
            if (other.CompareTag("Player"))
            {
                otherHealth.health -= normalBulletData.playerDamage;
            }
            else
            {
                otherHealth.health -= normalBulletData.otherDamage;
            }
        
        StopAllCoroutines();
        DeactivateBullet();
        return;
    }
}
