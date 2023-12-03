using UnityEngine;

[CreateAssetMenu(menuName = "SO/Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float movementSpeed;
    public float rotateSpeed;

    public int segmentOffset;
}