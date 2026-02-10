using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DraggableMaterial : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Correctness")]
    public bool isCorrectMaterial;

    [Header("Correct Slots")]
    public RectTransform[] correctSlots;

    public AudioClip clip;

    private RectTransform rectTransform;
    private RectTransform startParent;
    private Vector2 startAnchoredPos;
    private Canvas canvas;

    private Image image;
    private Color originalColor;

    public bool isLocked;
    public bool placedCorrectly { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startParent = rectTransform.parent as RectTransform;
        startAnchoredPos = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();

        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        rectTransform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        AudioManager.instance.PlaySpecificClip(clip);
        foreach (RectTransform slotTransform in correctSlots)
        {
            LeafSlot slot = slotTransform.GetComponent<LeafSlot>();

            if (slot == null || slot.isOccupied)
                continue;

            if (RectTransformUtility.RectangleContainsScreenPoint(
                slotTransform,
                Input.mousePosition,
                null))
            {
                if (isCorrectMaterial)
                {
                    StartCoroutine(BlinkGreenAndPlace(slotTransform, slot));
                    return;
                }
            }
        }
      
        // ❌ Wrong drop → blink first, then reset
        StartCoroutine(BlinkRedAndReset());
    }

    // 🟢 Correct placement
    IEnumerator BlinkGreenAndPlace(RectTransform slotTransform, LeafSlot slot)
    {
        image.color = Color.green;
        yield return new WaitForSeconds(0.15f);
        image.color = originalColor;

        rectTransform.SetParent(slotTransform, false);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;

        slot.isOccupied = true;
        placedCorrectly = true;
        isLocked = true;
    }

    // 🔴 Wrong placement
    IEnumerator BlinkRedAndReset()
    {
        image.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        image.color = originalColor;

        ResetPosition();
    }

    private void ResetPosition()
    {
        rectTransform.SetParent(startParent);
        rectTransform.anchoredPosition = startAnchoredPos;
    }
}
