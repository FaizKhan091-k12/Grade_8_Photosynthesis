using System;
using UnityEngine;
using DG.Tweening;
public class LeafSlotManager : MonoBehaviour
{
    [SerializeField] TypewriterTMP typewriterTMP;
    public LeafSlot[] slots;

    private bool triggered;
    [SerializeField] DraggableMaterial[] draggableMaterial;
    [SerializeField] private Transform continueBtnToVideoLevel;


    private void Start()
    {
        continueBtnToVideoLevel.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (!triggered && AllSlotsFilled())
        {
            triggered = true;
            Debug.Log("✅ All correct materials placed! Start Photosynthesis.");
            foreach (DraggableMaterial draggableMaterial in draggableMaterial)
            {
                draggableMaterial.isLocked = true;
            }

            Invoke(nameof(PlayLevelClear), 3f);
        }
    }

    public void PlayLevelClear()
    {  
        typewriterTMP.TypeText("Nice work! Now watch how these materials move to make food.",12f,
            ()=>continueBtnToVideoLevel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash));
        AudioManager.instance.PlayNiceWork();
        
    }
    private bool AllSlotsFilled()
    {
        foreach (LeafSlot slot in slots)
        {
            if (!slot.isOccupied)
                return false;
        }
        foreach (LeafSlot slot in slots)
        {
            if (slot.isOccupied)
                slot.GetComponent<ProceduralImageAlphaPingPong>().enabled = false;
        }
        return true;
    }
}