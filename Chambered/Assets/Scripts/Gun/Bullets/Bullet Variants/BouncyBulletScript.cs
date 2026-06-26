using UnityEngine;

public class BouncyBulletScript : BulletHandler
{
    [Header("Bullet Data")]
    public BulletValues bouncyBulletData;

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthScript otherHealth;

        otherHealth = other.GetComponent<HealthScript>();

        if (otherHealth != null && otherHealth.isDamaged)
            if (other.CompareTag("Player"))
            {
                otherHealth.health -= bouncyBulletData.playerDamage;
            }
            else
            {
                otherHealth.health -= bouncyBulletData.otherDamage;
            }
        

        StopAllCoroutines();
        DeactivateBullet();     
        return;
    }
}
