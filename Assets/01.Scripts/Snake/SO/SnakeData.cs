using UnityEngine;

[CreateAssetMenu(menuName = "SO/Data/SnakeData")]
public class SnakeData : ScriptableObject
{
    public float movementSpeed;
    public float rotateSpeed;

    public int segmentOffset;

    public int maxHealth;

    public float attackDelayMin;
    public float attackDelayMax;
    
    public float bulletSpeed;
}