using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnceEvent : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private bool isFired;

    public void FireAudio()
    {
        if (!isFired)
        {
            _audioSource.clip = _audioClip;
            _audioSource.PlayDelayed(1f);
            isFired = !isFired;
        }
    }
    
    
}