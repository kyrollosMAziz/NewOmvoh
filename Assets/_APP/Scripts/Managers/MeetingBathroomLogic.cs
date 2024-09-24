using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MeetingBathroomLogic : MonoBehaviour
{
    [SerializeField] AudioSource _meetingRoomAudioSource;
    [SerializeField] AudioClip _didntMakeItBackMale;
    [SerializeField] AudioClip _didntMakeItBackFemale;
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

    }
    public IEnumerator StartFemaleVoices()
    {
        yield return new WaitForSeconds(2);
        _meetingRoomAudioSource.PlayOneShot(_didntMakeItBackFemale);
        yield return new WaitForSeconds(_didntMakeItBackFemale.length + 1f);
        // Strong Haptics on stomach
    }
}
