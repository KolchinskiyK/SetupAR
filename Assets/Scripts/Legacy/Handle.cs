using UnityEngine;

// LEGACY slider script -> other concept now
public class Handle : MonoBehaviour
{
    public PlayUI p;
    public Transform menuAnchor;
    public Vector3 Offset;
    private Vector3 startPosition;
    private Quaternion startRotation;

    //public float maxSliderOffset = 1.0f;
    public float bassMin = 0.0f;
    public float bassMax = 1.0f;

    void OnEnable()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (startPosition.y + 1f >= transform.position.y && startPosition.y <= transform.position.y)
        {
            transform.position = new Vector3(
            startPosition.x,
            transform.position.y,
            startPosition.z
            );
        }
        else
        {
            transform.position = new Vector3(
            startPosition.x,
            startPosition.y,
            startPosition.z
            );
        }

            transform.rotation = startRotation;

        ChangeBass();

    }

    public void ChangeBass()
    {
        {
            float relativeHeight = transform.position.y - startPosition.y;

            relativeHeight = Mathf.Clamp(relativeHeight, 0f, 1f);

            float normalisedValue = relativeHeight / 1f;

            float bassValue = Mathf.Lerp(bassMin, bassMax, normalisedValue);

            Debug.Log("Success Musik Bass Slider! Bass: " + bassValue);

            p.OnSlider_Changed(bassValue);
        }
    }
}
