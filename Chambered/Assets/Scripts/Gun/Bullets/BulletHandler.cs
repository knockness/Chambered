using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [Header ("Physics Components")]
    public Rigidbody2D bulletRigidbody;

    [Header ("In Cylinder Check")]
    public bool isInCylinder;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        isInCylinder = false;
    }

    public void MoveBullet(float speed, Vector2 direction)
    {
        bulletRigidbody.linearVelocity = speed * direction.normalized;
    }

    protected void DeactivateBullet()
    {
        bulletRigidbody.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
