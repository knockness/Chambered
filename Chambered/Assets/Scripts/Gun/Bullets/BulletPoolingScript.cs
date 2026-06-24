using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPoolingScript : MonoBehaviour
{ 
    public BulletType bulletType;
    public enum BulletType
    {
        Normal,
        Bouncy,
        Poison,
        Exploding,
        Healing,
    } 

    [Header("Bullet Prefabs")]
    public BulletHandler normalBullet;
    public BulletHandler bouncyBullet;
    public BulletHandler poisonBullet;
    public BulletHandler explodingBullet;
    public BulletHandler healingBullet;

    [Header("Pool Settings")]
    public int bulletsPerPool;

    [Header("Bullet Pools")]
    private List<BulletHandler> normalPool = new List<BulletHandler>();
    private List<BulletHandler> bouncyPool = new List<BulletHandler>();
    private List<BulletHandler> poisonPool = new List<BulletHandler>();
    private List<BulletHandler> explodingPool = new List<BulletHandler>();
    private List<BulletHandler> healingPool = new List<BulletHandler>();
    
    [Header ("Script Access")]
    public GunStateScript gunStateScript;

    void Awake()
    {
        gunStateScript = GetComponent<GunStateScript>();
        
        MakeBulletList(normalBullet, normalPool);
        MakeBulletList(bouncyBullet, bouncyPool);
        MakeBulletList(poisonBullet, poisonPool);
        MakeBulletList(explodingBullet, explodingPool);
        MakeBulletList(healingBullet, healingPool);
    }

    void MakeBulletList(BulletHandler bulletPrefab, List<BulletHandler> poolType)
    {
        if (bulletPrefab == null)
            return;

        for (int i = 0; i < bulletsPerPool; i++)
        {
            BulletHandler spawned = Instantiate(bulletPrefab);
            spawned.gameObject.SetActive(false);
            poolType.Add(spawned);
        }
    }

    public BulletHandler FindDisabledBullet(BulletType type)
    {
        List<BulletHandler> poolList = type switch
        {
            BulletType.Normal => normalPool,
            BulletType.Bouncy => bouncyPool,
            BulletType.Poison => poisonPool,
            BulletType.Exploding => explodingPool,
            BulletType.Healing => healingPool,
            _ => null,
        };

        for(int i = 0; i < bulletsPerPool; i++)
        {
            if(!poolList[i].gameObject.activeSelf && !poolList[i].isInCylinder)
            {
                return poolList[i];
            }
        }

        return null;
    }
}