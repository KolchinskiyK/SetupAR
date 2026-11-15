using UnityEngine;
using UnityEngine.UI;

// LEGACY slider script -> other concept now
public class BassSlider : MonoBehaviour, SliderInteractable
{
    [Header("References")]
    public RectTransform sliderRect; // Slider
    public Image fillImage;          // Fill Image

    [Header("Sprites")]
    public Sprite emptySprite;
    public Sprite fullSprite;

    [Header("Value")]
    [Range(0f, 1f)]
    public float value;

    public void SetValueFromHit(RaycastHit hit)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            sliderRect,
            hit.point,
            null,
            out localPoint
        );

        float height = sliderRect.rect.height;

        value = Mathf.InverseLerp(
            -height * 0.5f,
             height * 0.5f,
             localPoint.y
        );

        value = Mathf.Clamp01(value);

        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        fillImage.fillAmount = value;
        fillImage.sprite = value > 0.01f ? fullSprite : emptySprite;
    }
}
