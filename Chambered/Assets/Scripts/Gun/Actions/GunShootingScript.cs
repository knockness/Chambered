using UnityEngine;

public class GunShootingScript : MonoBehaviour
{
    [Header ("Bullet Prefabs")]
    public GameObject neutralBullet;
    public GameObject bouncyBullet;
    public GameObject poisonBullets;
    public GameObject explosiveBullet;
    public GameObject healingBullet;

    [Header ("Fire Point")]
    public GameObject firePoint;

    [Header ("Script Access")]
    public GunStateScript gunStateScript;

    void Awake()
    {
        gunStateScript = GetComponent<GunStateScript>();
        
        firePoint = GameObject.Find("Fire Point");
    }

    void Update()
    {
        
    }
}
