using System;
using UnityEngine;
using DG.Tweening;

public class SingleDropManager : MonoBehaviour
{
    public SingleDraggable[] draggables;

    private bool triggered;
    [SerializeField] TypewriterTMP typewriterTMP;
    [SerializeField] private GameObject dialogue_Object;
    [SerializeField] private Animation animation;
    [SerializeField] private Transform summaryBtn;
    [SerializeField] private Transform summaryPanel;

    private void Start()
    {
        summaryBtn.transform.localScale = Vector3.zero;
        summaryPanel.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (!triggered && AllPlacedCorrectly())
        {
            triggered = true;
            Debug.Log("✅ All items placed correctly!");
            AudioManager.instance.LastClip();
            dialogue_Object.SetActive(true);
            typewriterTMP.TypeText("Excellent! You’ve shown how materials move through the plant to complete photosynthesis.",13f);
            summaryBtn.transform.localScale = Vector3.one;
            animation.enabled = true;
        }
    }

    public void ClickedSummaryBtn()
    {
        summaryPanel.transform.DOScale(Vector3.one, .25f).SetEase(Ease.OutBack);
        AudioManager.instance.PlayWellDone();
    }

    public void CloseSummaryPanel()
    {
        summaryPanel.transform.DOScale(Vector3.zero, .25f).SetEase(Ease.OutBack);
    }
    bool AllPlacedCorrectly()
    {
        foreach (SingleDraggable item in draggables)
        {
            if (!item.placedCorrectly)
                return false;
        }
        return true;
    }
}