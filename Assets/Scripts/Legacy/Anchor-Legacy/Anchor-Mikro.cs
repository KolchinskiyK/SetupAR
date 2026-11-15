using UnityEngine;

// LEGACY Room Achoring -> new Concept now
public class Anchor_Mikro : MonoBehaviour
{
    public Anchor_Data data;

    public void Initialize(Anchor_Data newData)
    {
        data = newData;

        gameObject.name = data.objectName;

        GetComponent<Renderer>().material.color = data.color;

        Debug.Log($"Init {data.objectName}: {data.valueA}, {data.valueB}");
    }
}
