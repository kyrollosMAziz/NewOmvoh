using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class pendetection : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioSource _maleAudioSource;
    [SerializeField] private AudioSource _femaleAudioSource;
    [SerializeField] GameData _gameData;
    bool isTaken;



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Pen") && !isTaken)
        {
            isTaken = !isTaken;
            StartCoroutine(StartPenAduio());
        }

    }

    private IEnumerator StartPenAduio()
    {
        AudioSource.Play();
        yield return new WaitForSeconds(AudioSource.clip.length);
        if (_gameData.playerGender == GenderEnum.Male)
            _maleAudioSource.Play();
        else
            _femaleAudioSource.Play();

        var time = _gameData.playerGender == GenderEnum.Male ? _maleAudioSource.clip.length : _femaleAudioSource.clip.length;
        yield return new WaitForSeconds(time + 5f);

        VignetteSceneLoadManager.Instance.LoadSceneByIndex(0);
    }
}
