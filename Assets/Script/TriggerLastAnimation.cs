using UnityEngine;

public class TriggerLastAnimation : MonoBehaviour
{
    [SerializeField] private MainMenuBehaviour mainMenuBehaviour;

    public void TriggerLastLevel()
    {
        mainMenuBehaviour.SunAnimTrue();
    }

    public void LabelPoP()
    {
        mainMenuBehaviour.StartLabels();
    }
}
