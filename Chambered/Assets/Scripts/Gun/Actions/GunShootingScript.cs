using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;
using System;

public class GunShootingScript : MonoBehaviour
{
    [Header ("Gun Variables")]
    public GameObject firePoint;
    public int chamberCount;
    public int MaxChamberCount;
    public List<BulletHandler> bulletsInCylinder = new List<BulletHandler>();

    [Header ("Script Access")]
    public GunStateScript gunStateScript;

    [Header("Input Actions")]
    public InputAction shootControls;
    public InputAction reloadControls;

    [Header("Bullet Types")]
    public BulletType bulletType;
    public BulletType[] bulletTypeArrayValues;


    void OnEnable()
    {
        reloadControls.Enable();
        shootControls.Enable();
    }

    void OnDisable()
    {
        reloadControls.Disable();
        shootControls.Disable();
    }

    void Awake()
    {
        bulletTypeArrayValues = (BulletType[])Enum.GetValues(typeof(BulletType));
        gunStateScript = GetComponent<GunStateScript>();
        
        firePoint = GameObject.Find("Fire Point");
        chamberCount = MaxChamberCount - 1;
    }

    void Update()
    {
        if(chamberCount < 0)
            chamberCount = MaxChamberCount - 1;

        if(shootControls.triggered)
        {
            ShootBullet();
        }

        if(reloadControls.triggered)
        {
            ReloadCylinder();
        }
        
    }

    BulletType GetRandomBulletType()
    {
        int randomIndex = UnityEngine.Random.Range(0, bulletTypeArrayValues.Length);
        return (BulletType)bulletTypeArrayValues.GetValue(randomIndex);
    }

    void ReloadCylinder()
    {
        bulletsInCylinder.Clear();
        while(bulletsInCylinder.Count < MaxChamberCount)
        {
            BulletHandler randomBullet = gunStateScript.bulletPoolingScript.FindDisabledBullet(GetRandomBulletType());

            bulletsInCylinder.Add(randomBullet);
            randomBullet.isInCylinder = true;
        }
    }

    void ShootBullet()
    {
        BulletHandler shotBullet = bulletsInCylinder[chamberCount];

        shotBullet.gameObject.transform.position = firePoint.transform.position;
        shotBullet.isInCylinder = false;

        float speed = shotBullet switch
        {
            NormalBulletScript => gunStateScript.normalBulletData.speed,
            BouncyBulletScript => gunStateScript.bouncyBulletData.speed,
            PoisonBulletScript => gunStateScript.poisonBulletData.speed,
            ExplodingBulletScript => gunStateScript.explodingBulletData.speed,
            HealingBulletScript => gunStateScript.healingBulletData.speed,
            _ => 10f
        };

        float lifetime = shotBullet switch
        {
            NormalBulletScript => gunStateScript.normalBulletData.lifetime,
            BouncyBulletScript => gunStateScript.bouncyBulletData.lifetime,
            PoisonBulletScript => gunStateScript.poisonBulletData.lifetime,
            ExplodingBulletScript => gunStateScript.explodingBulletData.lifetime,
            HealingBulletScript => gunStateScript.healingBulletData.lifetime,
            _ => 10
        };

        shotBullet.ActivateBullet(speed, gunStateScript.gunRotationScript.targetDirection, lifetime);

        bulletsInCylinder.RemoveAt(chamberCount);
        chamberCount -= 1;
    }
}
