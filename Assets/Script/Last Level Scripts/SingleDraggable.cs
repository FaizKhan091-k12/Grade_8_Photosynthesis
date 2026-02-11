using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class SingleDraggable : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Correct Drop Slot")]
    public RectTransform correctSlot;

    private RectTransform rectTransform;
    private RectTransform startParent;
    private Vector2 startAnchoredPos;
    private Canvas canvas;

    private Image image;
    private Color originalColor;

    private bool isLocked;
    public bool placedCorrectly { get; private set; }
    public GameObject lastHandDrag;
    public GameObject lastDialogue;
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
        lastHandDrag.SetActive(false);
        lastDialogue.SetActive(false);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        SingleDropSlot slot = correctSlot.GetComponent<SingleDropSlot>();

        if (slot != null && !slot.isOccupied &&
            RectTransformUtility.RectangleContainsScreenPoint(
                correctSlot,
                Input.mousePosition,
                null))
        {
            StartCoroutine(BlinkGreenAndPlace(slot));
        }
        else
        {
            StartCoroutine(BlinkRedAndReset());
        }
    }

    // 🟢 Correct placement
    IEnumerator BlinkGreenAndPlace(SingleDropSlot slot)
    {
        image.color = Color.green;
        yield return new WaitForSeconds(0.15f);
        image.color = originalColor;

        rectTransform.SetParent(correctSlot, false);
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
