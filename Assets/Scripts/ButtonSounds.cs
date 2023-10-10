using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip howerSound, ñlickSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HowerSoundEffect()
    {
        audioSource.PlayOneShot(howerSound);
    }

    public void ClickSoundEffect()
    {
        audioSource.PlayOneShot(ñlickSound);
    }

}
