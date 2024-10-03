using Bhaptics.SDK2;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class pendetection : MonoBehaviour
{

    [SerializeField] private PlayGlow playGlow1;
    [SerializeField] private XRGrabInteractable _pen1;

    [SerializeField] private PlayGlow playGlow2;
    [SerializeField] private XRGrabInteractable _pen2;


    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioSource _maleAudioSource;
    [SerializeField] private AudioSource _femaleAudioSource;
    [SerializeField] GameData _gameData;
    bool isTaken;
    int penCount;
    private void Start()
    {
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.CALM);
        playGlow1.Glow();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Pen") && !isTaken)
        {
            AudioSource.Play();

            penCount++;
            if (penCount == 1)
            {
                _pen1.gameObject.tag = "Untagged";
                playGlow1.ResetMaterial();
                StartCoroutine(ResetPen1Position());
            }

            if (penCount == 2)
            {
                _pen2.gameObject.tag = "Untagged";
                StartCoroutine(ResetPen2Position());
                isTaken = !isTaken;
            }
        }
    }

    private IEnumerator ResetPen1Position()
    {
        yield return new WaitForSeconds(1.5f);
        _pen1.enabled = false;
        playGlow1.ResetMaterial();

        _pen2.enabled = true;
        playGlow2.Glow();
    }
    private IEnumerator ResetPen2Position()
    {
        yield return new WaitForSeconds(1.5f);
        _pen2.enabled = false;
        playGlow2.ResetMaterial();
        StartCoroutine(StartPenAduio());
    }
    private IEnumerator StartPenAduio()
    {
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
