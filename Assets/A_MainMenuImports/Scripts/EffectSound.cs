using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    private AudioSource audioSource;
    private int audioNum = 0;

    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioNum = audioClips.Length;
    }

    public void EffectPlay()
    {
        audioSource.clip = audioClips[Random.Range(0, audioNum)];
        audioSource.Play();
    }
}
