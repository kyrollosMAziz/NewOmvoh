using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MeetingBathroomLogic : MonoBehaviour
{
    [SerializeField] AudioSource _meetingRoomAudioSource;
    [SerializeField] AudioClip _didntMakeItBackMale;
    [SerializeField] AudioClip _didntMakeItBackFemale;
    [SerializeField] AudioClip _howDoIExplainMale;
    [SerializeField] AudioClip _howDoIExplainFemale;
    [SerializeField] AudioClip _thisCouldRuinMale;
    [SerializeField] AudioClip _thisCouldRuinFemale;
    [SerializeField] GameData _gameData;

    void Start()
    {
        AudioManagerMain.instance.PlaySFX("RunningWaterSFX");

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
        _meetingRoomAudioSource.PlayOneShot(_didntMakeItBackMale);
        yield return new WaitForSeconds(_didntMakeItBackMale.length + 1f);
        // Strong Haptics on stomach

        VignetteFadeController.Instance.FadeImageOut();
        AudioManagerMain.instance.PlaySFX("BreathSlowSFX");

        _meetingRoomAudioSource.PlayOneShot(_howDoIExplainMale);
        yield return new WaitForSeconds(_howDoIExplainMale.length + 3f);


        AudioManagerMain.instance.StopSound("BreathSlowSFX");
        _meetingRoomAudioSource.PlayOneShot(_thisCouldRuinMale);
        yield return new WaitForSeconds(_thisCouldRuinMale.length + 1f);

        //Scene ending

    }
    public IEnumerator StartFemaleVoices()
    {
        yield return new WaitForSeconds(2);
        _meetingRoomAudioSource.PlayOneShot(_didntMakeItBackFemale);
        yield return new WaitForSeconds(_didntMakeItBackFemale.length + 1f);
        // Strong Haptics on stomach

        VignetteFadeController.Instance.FadeImageOut();
        AudioManagerMain.instance.PlaySFX("BreathSlowSFX");

        _meetingRoomAudioSource.PlayOneShot(_howDoIExplainFemale);
        yield return new WaitForSeconds(_howDoIExplainFemale.length + 3f);

        AudioManagerMain.instance.StopSound("BreathSlowSFX");
        _meetingRoomAudioSource.PlayOneShot(_thisCouldRuinFemale);
        yield return new WaitForSeconds(_thisCouldRuinFemale.length + 3f);

        //Scene ending
    }
}
