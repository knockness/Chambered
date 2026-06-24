using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using Unity.VisualScripting;

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

    [Header("Bullet Type Enum Instance")]
    public BulletType bulletType;

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
        Array values = Enum.GetValues(typeof(BulletType));

        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        return (BulletType)values.GetValue(randomIndex);
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
        shotBullet.gameObject.SetActive(true);

        float speed = shotBullet switch
        {
            NormalBulletScript => gunStateScript.normalBulletData.speed,
            BouncyBulletScript => gunStateScript.bouncyBulletData.speed,
            PoisonBulletScript => gunStateScript.poisonBulletData.speed,
            ExplodingBulletScript => gunStateScript.explodingBulletData.speed,
            HealingBulletScript => gunStateScript.healingBulletData.speed,
            _ => 10f
        };

        shotBullet.MoveBullet(speed, gunStateScript.gunRotationScript.targetDirection);

        bulletsInCylinder.RemoveAt(chamberCount);
        chamberCount -= 1;
    }
}
