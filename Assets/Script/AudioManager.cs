using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] public AudioSource audioSource_BG;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private AudioClip introClip;
    [SerializeField] AudioClip chooseClip;
    [SerializeField] AudioClip niceWorkClip;
    [SerializeField] AudioClip lastDialogue;
    [SerializeField] private AudioClip lastClip;
    [SerializeField] private AudioClip wellDone;
   

    private void Awake()
    {
        instance = this;
    }

    public void PlayIntro()
    {
        audioSource.PlayOneShot(introClip);
    }

    public void PlayChoose()
    {
        audioSource.PlayOneShot(chooseClip);
    }

  
    public void PlayNiceWork()
    {
        audioSource.PlayOneShot(niceWorkClip);
    }
    public void PlaySpecificClip(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
           
        }
        audioSource.PlayOneShot(clip);
    }

    public void PlayLastDialogue()
    {
        audioSource.PlayOneShot(lastDialogue);
    }

    public void LastClip()
    {
        audioSource.PlayOneShot(lastClip);
    }

    public void PlayWellDone()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(wellDone);
    }
}
