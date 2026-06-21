using UnityEngine;

public class GunPickupScript : MonoBehaviour
{
    public GameObject gunClampPoint;
    public BoxCollider2D pickupCollider; 

    [Header ("Script Access")]
    public GunStateScript gunStateScript;

    void Awake()
    {
        gunStateScript = GetComponent<GunStateScript>();

        gunClampPoint = GameObject.Find("Gun Clamp Point");
        pickupCollider = GetComponent<BoxCollider2D>();
        pickupCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            pickupCollider.enabled = false;
            transform.position = gunClampPoint.transform.position;
            transform.rotation = gunClampPoint.transform.rotation;
            transform.SetParent(gunClampPoint.transform);
            gunStateScript.state = GunStateScript.GunState.Neutral;
        }
    }
}