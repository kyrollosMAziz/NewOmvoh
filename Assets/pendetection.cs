using Bhaptics.SDK2;
using System;
using System.Collections;
using UnityEngine;

public class pendetection : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioSource _maleAudioSource;
    [SerializeField] private AudioSource _femaleAudioSource;
    [SerializeField] GameData _gameData;
    bool isTaken;

    private void Start()
    {
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.CALM);
    }

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
        HaptticManager.Instance.StopHapticLoop();
        VignetteSceneLoadManager.Instance.LoadSceneByIndex(0);
    }
}
