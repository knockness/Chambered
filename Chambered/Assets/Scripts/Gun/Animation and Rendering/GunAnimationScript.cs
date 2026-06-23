using UnityEngine;

public class GunAnimationScript : MonoBehaviour
{
    [Header("States")]
    public GunStateScript.GunState previouseState;

    [Header ("Script Access")]
    public GunStateScript gunStateScript;

    [Header("Animation Components")]
    public Animator gunAnimator;

    void Awake()
    {
        gunStateScript = GetComponentInParent<GunStateScript>();
    }

    void Update()
    {
//This is to make sure the switch statement only runs when the state actually switches, preventing animations for replaying over and over.
        if(previouseState == gunStateScript.state)
            return;
            
        previouseState = gunStateScript.state;

        switch(gunStateScript.state)
        {
            case GunStateScript.GunState.Disabled:
            case GunStateScript.GunState.Neutral:
                break;

            case GunStateScript.GunState.Chambering:
                gunAnimator.Play("chamberBullet");
                break;

            case GunStateScript.GunState.Reloading:
                gunAnimator.Play("reloadCylinder");
                break;
        }
    }
}
