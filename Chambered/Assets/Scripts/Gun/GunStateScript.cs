using UnityEngine;

//Globalize the bullet type
    public enum BulletType
    {
        Normal,
        Bouncy,
        Poison,
        Exploding,
        Healing,
    } 

public class GunStateScript : MonoBehaviour
{
    [Header ("Script Access")]
    public GunPickupScript gunPickupScript;
    public GunShootingScript gunShootingScript;
    public GunAnimationScript gunAnimationScript;
    public BulletPoolingScript bulletPoolingScript;
    public GunRotationScript gunRotationScript;

    [Header("Bullet Data")]
    public BulletValues normalBulletData;
    public BulletValues bouncyBulletData;
    public BulletValues poisonBulletData;
    public BulletValues explodingBulletData;
    public BulletValues healingBulletData;

    [Header ("Object Access")]
    public GameObject gunRotationPoint;

    [Header ("Switch State Permission Booleans")]
    public bool debugIsBusy; //This is just here because isBusy will not show in the inspector due to the getter.
    public bool isBusy => state == GunState.Disabled 
                       || state == GunState.Chambering
                       || state == GunState.Reloading;

    [Header ("Public Gun State")]
    public GunState state;

    void Awake()
    {
        gunPickupScript = GetComponent<GunPickupScript>();
        gunShootingScript = GetComponent<GunShootingScript>();
        gunAnimationScript = GetComponentInChildren<GunAnimationScript>();
        bulletPoolingScript = GetComponent<BulletPoolingScript>();

        gunRotationPoint = GameObject.Find("Gun Rotation Point");
        gunRotationScript = gunRotationPoint.GetComponent<GunRotationScript>();
    }

    void Update()
    {
        debugIsBusy = isBusy;
    }

    public enum GunState
    {
        Disabled,
        Neutral,
        Chambering,
        Reloading
    }
}
