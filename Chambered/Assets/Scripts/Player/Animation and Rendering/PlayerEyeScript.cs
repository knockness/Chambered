using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEyeScript : MonoBehaviour
{
    [Header("Mouse Position")]
    public Vector2 mouseScreenPosition;
    public Vector2 mouseWorldPosition;

    [Header("Eye Position Variables")]
    public float maxDistanceFromCenter;
    public Vector2 centerPosition;
    public Vector2 targetDirection;
    public Vector2 offset;

    void Awake()
    {
        centerPosition = transform.localPosition;
    }

    void Update()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue(); 
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        targetDirection = mouseWorldPosition - (Vector2)transform.parent.position;

        offset = Vector2.ClampMagnitude(targetDirection, maxDistanceFromCenter);
        transform.localPosition = Vector3.MoveTowards(centerPosition, centerPosition + offset, maxDistanceFromCenter);
    }
}
