using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunRotationScript : MonoBehaviour
{
    [Header("Mouse Position")]
    public Vector2 mouseScreenPosition;
    public Vector2 mouseWorldPosition;

    [Header("Direction Variables")]
    public Vector2 targetDirection;

    [Header("Transform Components")]
    public Transform gunClampTransform;

    void Update()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue(); 
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        targetDirection = mouseWorldPosition - (Vector2)transform.parent.position;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg);

        if(Vector2.Dot(Vector2.right, targetDirection) < 0)
        {
            gunClampTransform.localRotation = quaternion.Euler(180 * Mathf.Deg2Rad, 0, 0);
        } else
        {
            gunClampTransform.localRotation = quaternion.Euler(0, 0, 0);
        }
    }
}
