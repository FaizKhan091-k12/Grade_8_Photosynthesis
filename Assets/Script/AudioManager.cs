using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private AudioClip introClip;
    [SerializeField] AudioClip chooseClip;
    [SerializeField] AudioClip niceWorkClip;
   

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
}
