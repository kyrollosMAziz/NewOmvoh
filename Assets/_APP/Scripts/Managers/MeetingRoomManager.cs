using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingRoomManager : MonoBehaviour
{
    [SerializeField] AudioSource _meetingRoomAudioSource;
    [SerializeField] AudioClip _halfwayPresentationMale;
    [SerializeField] AudioClip _halfwayPresentationFemale;
    [SerializeField] AudioClip _thisCantHappenMale;
    [SerializeField] AudioClip _thisCantHappenFemale;
    [SerializeField] AudioClip _haveToGoMale;
    [SerializeField] AudioClip _haveToGoFemale;
    [SerializeField] GameData _gameData;

    void Start()
    {
        AudioManagerMain.instance.PlaySFX("MeetingBgSFX");

        if (_gameData.playerGender == GenderEnum.Female)
        {
            StartCoroutine(StartFemaleVoices());
        }
        else if (_gameData.playerGender == GenderEnum.Male)
        {
            StartCoroutine(StartMaleVoices());
        }
    }

    public IEnumerator StartMaleVoices()
    {
        yield return new WaitForSeconds(2);
        _meetingRoomAudioSource.PlayOneShot(_halfwayPresentationMale);
        yield return new WaitForSeconds(_halfwayPresentationMale.length + 1f);
        AudioManagerMain.instance.PlaySFX("HeartbeatSFX");
        _meetingRoomAudioSource.PlayOneShot(_thisCantHappenMale);
        yield return new WaitForSeconds(_thisCantHappenMale.length + 1f);
        // Visual + Haptics

        AudioManagerMain.instance.PlaySFX("UserBreathing");
        _meetingRoomAudioSource.PlayOneShot(_haveToGoMale);
        yield return new WaitForSeconds(_haveToGoMale.length + 1f);

        VignetteSceneLoadManager.Instance.LoadSceneByName("MeetingBathroom");
        AudioManagerMain.instance.PlaySFX("FootSteps");
    }
    public IEnumerator StartFemaleVoices()
    {
        yield return new WaitForSeconds(2);
        _meetingRoomAudioSource.PlayOneShot(_halfwayPresentationFemale);
        yield return new WaitForSeconds(_halfwayPresentationFemale.length + 1f);
        AudioManagerMain.instance.PlaySFX("HeartbeatSFX");
        _meetingRoomAudioSource.PlayOneShot(_thisCantHappenFemale);
        yield return new WaitForSeconds(_thisCantHappenFemale.length + 1f);
        // Visual + Haptics

        AudioManagerMain.instance.PlaySFX("UserBreathing");
        _meetingRoomAudioSource.PlayOneShot(_haveToGoFemale);
        yield return new WaitForSeconds(_haveToGoFemale.length + 1f);

        VignetteSceneLoadManager.Instance.LoadSceneByName("MeetingBathroom");
        AudioManagerMain.instance.PlaySFX("FootSteps");
    }
}
