using UnityEngine;
using UnityEngine.Events;

// LEGACY slider script -> other concept now
public class Slider : MonoBehaviour
{
    [Header("Slider Settings")]
    public Transform handle;
    public PlayUI p;
    public float minY = 0f;
    public float maxY = 1f;

    [Range(0f, 1f)]
    public float value;

    public UnityEvent<float> OnValueChanged;

    void Update()
    {
        float y = Mathf.Clamp(handle.localPosition.y, minY, maxY);
        value = Mathf.InverseLerp(minY, maxY, y);
        OnValueChanged?.Invoke(value);
        p.OnSlider_Changed(value);
    }

}
