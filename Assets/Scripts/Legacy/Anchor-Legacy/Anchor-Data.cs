using UnityEngine;

// LEGACY Room Achoring -> new Concept now

[CreateAssetMenu(menuName = "Calc/Calc Object Data")]
public class Anchor_Data : ScriptableObject
{
    public string objectName;
    public float valueA;
    public float valueB;
    public Color color;
}
