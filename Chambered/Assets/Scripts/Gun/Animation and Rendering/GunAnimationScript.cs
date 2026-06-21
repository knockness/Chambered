using UnityEngine;

public class GunAnimationScript : MonoBehaviour
{
    [Header ("Script Access")]
    public GunStateScript gunStateScript;

    void Awake()
    {
        gunStateScript = GetComponentInParent<GunStateScript>();
    }

    void Update()
    {
        
    }
}
