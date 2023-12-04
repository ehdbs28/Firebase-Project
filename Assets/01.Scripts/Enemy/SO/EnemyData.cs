using UnityEngine;

[CreateAssetMenu(menuName = "SO/Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float movementSpeed;
    public float rotateSpeed;
    public int maxHealth;
}