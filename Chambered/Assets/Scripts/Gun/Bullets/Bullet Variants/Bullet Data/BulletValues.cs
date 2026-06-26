using UnityEngine;

[CreateAssetMenu(fileName = "BulletValues", menuName = "Scriptable Objects/BulletValues")]
public class BulletValues : ScriptableObject
{
    public float speed;
    public float otherDamage;
    public float playerDamage;
    public float lifetime;
}
