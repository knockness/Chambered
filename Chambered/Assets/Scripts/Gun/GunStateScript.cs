using UnityEngine;

public class GunStateScript : MonoBehaviour
{
    [Header ("Script Access")]
    public GunPickupScript gunPickupScript;
    public GunShootingScript gunShootingScript;
    public GunAnimationScript gunAnimationScript;

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
