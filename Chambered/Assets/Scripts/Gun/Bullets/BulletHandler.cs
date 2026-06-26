using System.Collections;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [Header("Bullet Type Enum Instance")]
    public BulletType bulletType;

    [Header ("Physics")]
    public Rigidbody2D bulletRigidbody;

    [Header ("In Cylinder Check")]
    public bool isInCylinder;


    protected virtual void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        isInCylinder = false;
    }

    public void ActivateBullet(float speed, Vector2 direction, float lifetime)
    {
        gameObject.SetActive(true);
        bulletRigidbody.linearVelocity = speed * direction.normalized;
        StartCoroutine(BulletLifetimeCoroutine(lifetime));
    }

    protected void DeactivateBullet()
    {
        bulletRigidbody.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public IEnumerator BulletLifetimeCoroutine(float lifetimeSeconds)
    {
        yield return new WaitForSeconds(lifetimeSeconds);
        DeactivateBullet();
    }
}
