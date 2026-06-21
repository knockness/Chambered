using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEyeScript : MonoBehaviour
{
    [Header("Mouse Position")]
    public Vector3 mouseScreenPosition;
    public Vector3 mouseWorldPosition;

    [Header("Eye Position Variables")]
    public float maxDistanceFromCenter;
    public Vector3 centerPosition;
    public Vector3 targetDirection;
    public Vector3 offset;

    void Awake()
    {
        centerPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -1);
    }

    void Update()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue(); 
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        targetDirection = mouseWorldPosition - transform.parent.position;

        offset = Vector2.ClampMagnitude(targetDirection, maxDistanceFromCenter);
        transform.localPosition = Vector3.MoveTowards(centerPosition, centerPosition + offset, maxDistanceFromCenter);
    }
}