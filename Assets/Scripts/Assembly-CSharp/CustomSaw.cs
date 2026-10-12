using UnityEngine;
using UnityEngine.UI;

public class CustomSaw : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image fillerImage;
    [SerializeField] private RectTransform fillerRect;
    [SerializeField] private RectTransform sawRect;

    [Header("Saw")]
    [SerializeField] private float rotationSpeed = 600f;
    [SerializeField] private float sawXOffset = 0f;

    private float sawY;

    private void Awake()
    {
        if (sawRect == null)
        {
            sawRect = GetComponent<RectTransform>();
        }

        if (fillerRect == null && transform.parent != null)
        {
            fillerRect = transform.parent as RectTransform;
        }

        if (fillerImage == null && fillerRect != null)
        {
            fillerImage = fillerRect.GetComponent<Image>();
        }

        if (fillerImage != null)
        {
            // Required for fillAmount.
            fillerImage.type = Image.Type.Filled;
            fillerImage.fillMethod = Image.FillMethod.Horizontal;
            fillerImage.fillOrigin =
                (int)Image.OriginHorizontal.Left;

            fillerImage.fillAmount = 0f;
        }

        if (sawRect != null)
        {
            // Saw moves relative to LEFT side of filler.
            sawRect.anchorMin = new Vector2(0f, 0.5f);
            sawRect.anchorMax = new Vector2(0f, 0.5f);
            sawRect.pivot = new Vector2(0.5f, 0.5f);

            sawY = sawRect.anchoredPosition.y;
        }

        Canvas.ForceUpdateCanvases();

        UpdateVisuals(0f);
    }

    private void Update()
    {
        if (sawRect != null)
        {
            // Continuous red saw rotation.
            sawRect.Rotate(
                0f,
                0f,
                -rotationSpeed * Time.unscaledDeltaTime
            );
        }

        float progress = 0f;

        if (Preloader.Instance != null)
        {
            progress = Preloader.Instance.visualProgress;
        }
        else if (fillerImage != null)
        {
            progress = fillerImage.fillAmount;
        }

        UpdateVisuals(progress);
    }

    private void UpdateVisuals(float progress)
    {
        progress = Mathf.Clamp01(progress);

        // Fill LEFT -> RIGHT.
        if (fillerImage != null)
        {
            fillerImage.fillAmount = progress;
        }

        // Saw follows the exact front edge of the filler.
        if (sawRect != null && fillerRect != null)
        {
            float width = fillerRect.rect.width;

            float sawX =
                (width * progress) + sawXOffset;

            sawRect.anchoredPosition =
                new Vector2(sawX, sawY);
        }
    }
}