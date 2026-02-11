using UnityEngine;

public class SingleDropManager : MonoBehaviour
{
    public SingleDraggable[] draggables;

    private bool triggered;
    public GameObject particles_Confetti;
    void Update()
    {
        if (!triggered && AllPlacedCorrectly())
        {
            triggered = true;
            Debug.Log("✅ All items placed correctly!");
            AudioManager.instance.LastClip();
            particles_Confetti.SetActive(true);
        }
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