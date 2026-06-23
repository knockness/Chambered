using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPoolingScript : MonoBehaviour
{ 
    public enum BulletType
    {
        Normal,
        Bouncy,
        Poison,
        Exploding,
        Healing,
    } 

    [Header("Bullet Prefabs")]
    private BulletHandler normalBullet;
    private BulletHandler bouncyBullet;
    private BulletHandler poisonBullet;
    private BulletHandler explodingBullet;
    private BulletHandler healingBullet;

    [Header("Pool Settings")]
    public int bulletsPerPool;

    [Header("Bullet Pools")]
    private List<BulletHandler> normalPool = new List<BulletHandler>();
    private List<BulletHandler> bouncyPool = new List<BulletHandler>();
    private List<BulletHandler> poisonPool = new List<BulletHandler>();
    private List<BulletHandler> explodingPool = new List<BulletHandler>();
    private List<BulletHandler> healingPool = new List<BulletHandler>();

    void Awake()
    {
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
            if(!poolList[i].gameObject.activeSelf)
            {
                return poolList[i];
            }
        }

        return null;
    }

    void Update()
    {

    }
}