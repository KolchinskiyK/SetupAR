using UnityEngine;

// LEGACY Room Achoring -> new Concept now
public class Anchor_Spawn : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject derivedPrefab;

    [Header("Offsets")]
    public Vector3[] spawnOffsets;

    [Header("Data")]
    public Anchor_Data[] dataSet;

    private GameObject[] spawnedObjects;

    void Start()
    {
        SpawnDerivedObjects();
    }

    void SpawnDerivedObjects()
    {
        int count = Mathf.Min(spawnOffsets.Length, dataSet.Length);
        spawnedObjects = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 worldPos =
                transform.position +
                transform.rotation * spawnOffsets[i];

            GameObject obj = Instantiate(
                derivedPrefab,
                worldPos,
                transform.rotation
            );

            Anchor_Mikro calcObj = obj.GetComponent<Anchor_Mikro>();
            calcObj.Initialize(dataSet[i]);

            spawnedObjects[i] = obj;
        }
    }
}



