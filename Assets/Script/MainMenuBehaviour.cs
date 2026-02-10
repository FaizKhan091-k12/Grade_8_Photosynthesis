    using System;
    using System.Collections;
    using UnityEngine;
    using DG.Tweening;
    using TMPro;
    using UnityEngine.UI.ProceduralImage;

    public class MainMenuBehaviour : MonoBehaviour
{    
    [Header("Main Menu Properties")]
    [SerializeField] private Transform photosynthesis_TextImage;
    [SerializeField] private Transform factory_TextImage;
    [SerializeField] private Transform sun;
    [SerializeField] private Transform leaf;
    [SerializeField] private Transform startButton;
    [SerializeField] private Transform sunRays;
    [SerializeField] private float rotSpeed = 15f;
    [SerializeField] private Transform mute, unMute;
    [SerializeField] bool isMuted;
    
    [Header("Intro Level Properties")]
    [SerializeField] private GameObject leafImage;
    [SerializeField] private GameObject leafImageParent;
    [SerializeField] private float IndroductionLeafSpeed;
    [SerializeField] Transform introductionTopText;
    [SerializeField] AnimationCurve easeCurve;
    [SerializeField] TypewriterTMP typewriterTMP;
    [SerializeField] private GameObject dialogue_Container;
    [SerializeField] Transform continue_Button;
    [SerializeField] private GameObject newLeaf;
    [Header("Drag Level Properties")] [SerializeField]
    private GameObject dragLevel;

    [SerializeField] private Transform oxygen, glucose, heat, water, co2, light;
    
    [Header("Video Level Properties")]
    [SerializeField] GameObject videoLevel;

    [SerializeField] private GameObject cloudGenerator;
    [SerializeField] private GameObject sunflower;


    private void Awake()
    {
        photosynthesis_TextImage.transform.localScale = Vector3.zero;
        factory_TextImage.transform.localScale = Vector3.zero;
        sun.transform.localScale = Vector3.zero;    
        leaf.transform.localScale = Vector3.zero;
        startButton.transform.localScale = Vector3.zero;
        mute.gameObject.SetActive(isMuted);
        unMute.transform.localScale = Vector3.one;
        introductionTopText.transform.localScale = Vector3.zero;    
        factory_TextImage.gameObject.SetActive(false);
        dialogue_Container.SetActive(false);
        continue_Button.transform.localScale = Vector3.zero;
        // Drag Level Start Here

        dragLevel.transform.localScale = Vector3.zero;
        
        
        oxygen.transform.localScale = Vector3.zero;
        glucose.transform.localScale = Vector3.zero;
        heat.transform.localScale = Vector3.zero;
        water.transform.localScale = Vector3.zero;
        light.transform.localScale = Vector3.zero;
        co2.transform.localScale = Vector3.zero;
    
        // Video Level Start Here
        
        videoLevel.transform.localScale = Vector3.zero;
        cloudGenerator.SetActive(false);
        sunflower.SetActive(false);
    }

    private void Start()
    {
        InitiateStartScreen();
    }

    private void Update()
    {
        sunRays.Rotate(0, 0, rotSpeed * Time.deltaTime);

      
    }

    public void InitiateStartScreen()
    {
        photosynthesis_TextImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        factory_TextImage.gameObject.SetActive(true);
        Invoke(nameof(LateFactoryText), .3f);
    }

    public void LateFactoryText()
    {
        factory_TextImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(SunScale), .3f);
    }
    public void SunScale()
    {
        sun.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(LeafScale), .3f);
    }

    public void LeafScale()
    {
        leaf.transform.DOScale(new Vector3(4.215408f,4.215408f,4.215408f), 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(StartButtonScale), .3f);
    }

    public void StartButtonScale()
    {
        startButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
    }

    public void SoundControllers()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            unMute.gameObject.SetActive(false);
            mute.gameObject.SetActive(true);
            unMute.transform.localScale = Vector3.zero;
            mute.transform.localScale = Vector3.zero;
            mute.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutFlash); 
        }
        else
        {
            unMute.gameObject.SetActive(true);
            mute.gameObject.SetActive(false);
            mute.transform.localScale = Vector3.zero;
            unMute.transform.localScale = Vector3.zero;
            unMute.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutFlash); 
        }
    }
 // After Start Button Click ----
    public void StartExperimentClick()
    {
        startButton.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(FactoyButtonScaleZero),.3f);
    }

    void FactoyButtonScaleZero()
    {
        leaf.gameObject.SetActive(false); 
        newLeaf = Instantiate(leafImage, leafImageParent.transform);
        newLeaf.SetActive(true);
        newLeaf.transform.localPosition = new Vector3(newLeaf.transform.localPosition.x, 145, newLeaf.transform.localPosition.z);
        newLeaf.GetComponent<ProceduralImage>().raycastTarget = false;
     
        StartCoroutine(IntroductionLeaf(newLeaf));
        factory_TextImage.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(StartButtonScaleZero), .3f);
    }

    void StartButtonScaleZero()
    {   
   
        photosynthesis_TextImage.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutFlash);
    }

    IEnumerator IntroductionLeaf(GameObject leaf)
    {
        float duration = 1.5f; // smooth, cinematic
        float elapsed = 0f;

        Vector3 startScale = leaf.transform.localScale;
        Vector3 startPosition = leaf.transform.localPosition;
        Quaternion startRotation = leaf.transform.localRotation;

        Vector3 targetScale = new Vector3(8.537887f, 8.537887f, 8.537887f);
        Vector3 targetPosition = Vector3.zero;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -30f);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            // THIS is what makes it smooth
            float smoothT = easeCurve.Evaluate(t);


            leaf.transform.localScale =
                Vector3.Lerp(startScale, targetScale, smoothT);

            leaf.transform.localPosition =
                Vector3.Lerp(startPosition, targetPosition, smoothT);

            leaf.transform.localRotation =
                Quaternion.Lerp(startRotation, targetRotation, smoothT);

            yield return null;
        }

        // Snap exactly to final values
        leaf.transform.localScale = targetScale;
        leaf.transform.localPosition = targetPosition;
        leaf.transform.localRotation = targetRotation;

      IntroText();
     
    }


    void IntroText()
    {
    
        introductionTopText.transform.localScale = Vector3.zero;
        introductionTopText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(DialogueEnableIntro),1f);
       
    }

    void DialogueEnableIntro()
    {
        dialogue_Container.SetActive(true);
        typewriterTMP.TypeText("Plants make food using photosynthesis. Let’s see what they need.",13f,()=>ContinueButton());
        AudioManager.instance.PlayIntro();
        
    }

    void ContinueButton()
    {
        continue_Button.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
    }
    
    // Drag Level Start From Here 
    public void ContinueButtonClicked()
    {

        introductionTopText.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutFlash);
        continue_Button.transform.DOScale(Vector3.zero, .2f).SetEase(Ease.InOutFlash);
        dialogue_Container.SetActive(false);
        newLeaf.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutFlash);
        continue_Button.transform.DOScale(Vector3.zero, .2f).SetEase(Ease.InOutFlash);
        Invoke(nameof(DragLevelStart), .3f);

    }

    public void DragLevelStart()
    {
         dragLevel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
         Invoke(nameof(DragLevelDialogue),.5f);
    }

    public void DragLevelDialogue()
    {
        dialogue_Container.SetActive(true);
        typewriterTMP.TypeText("Choose the materials that plants use to start photosynthesis.",13f,()=>OxygenIcon());
        AudioManager.instance.PlayChoose();
    }

    void OxygenIcon()
    {
        oxygen.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(GlucoseIcon),.2f);
    }
    void GlucoseIcon()
    {
        glucose.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(CarbonDiIcon),.2f);
    }
    void CarbonDiIcon()
    {
        co2.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(WaterIcon),.2f);
    }
    void WaterIcon()
    {
        water.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(LightIcon),.2f);
    }
    void LightIcon()
    {
        light.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        Invoke(nameof(HeatIcon),.2f);
    }
    void HeatIcon()
    {
        heat.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
    }

    // Video Level Start Here

    //On Button CLicked
    public void ClickedContinueToVideoLevel()
    {
        videoLevel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutFlash);
        cloudGenerator.SetActive(true);
        sunflower.SetActive(true);
    }
    
}
