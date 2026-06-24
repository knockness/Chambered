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
            BulletHandler shotBullet = bulletsInCylinder[chamberCount];

            shotBullet.gameObject.transform.position = firePoint.transform.position;
            shotBullet.isInCylinder = false;
            shotBullet.gameObject.SetActive(true);

            shotBullet.MoveBullet(1, gunStateScript.gunRotationScript.targetDirection);

            bulletsInCylinder.RemoveAt(chamberCount);
            chamberCount -= 1;
        }

        if(reloadControls.triggered)
        {
            ReloadCylinder();
        }
        
    }

    BulletPoolingScript.BulletType GetRandomBulletType()
    {
        Array values = Enum.GetValues(typeof(BulletPoolingScript.BulletType));

        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        return (BulletPoolingScript.BulletType)values.GetValue(randomIndex);
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
}
