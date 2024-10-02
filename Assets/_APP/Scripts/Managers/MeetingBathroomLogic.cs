using Bhaptics.SDK2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MeetingBathroomLogic : MonoBehaviour
{
    [SerializeField] AudioSource _meetingRoomAudioSource;

    [Header("SFX")]
    [SerializeField] AudioSource _runningWaterSFX;
    [SerializeField] AudioSource _footstepsEchoSFX;
    [SerializeField] AudioSource _heartbeatSlowSFX;
    [SerializeField] AudioSource _shitBgSFX;

    [Header("Make It Back")]
    [SerializeField] AudioClip _makeItBackMale;
    [SerializeField] AudioClip _makeItBackFemale;

    [Header("My Coworkers")]
    [SerializeField] AudioClip _myCoworkersMale;
    [SerializeField] AudioClip _myCoworkersFemale;

    [Header("How do I explain")]
    [SerializeField] AudioClip _howDoIExplainMale;
    [SerializeField] AudioClip _howDoIExplainFemale;

    [Header("This Could Ruin")]
    [SerializeField] AudioClip _thisCouldRuinMale;
    [SerializeField] AudioClip _thisCouldRuinFemale;

    [SerializeField] GameData _gameData;

    void Start()
    {
        _runningWaterSFX.Play();
        _footstepsEchoSFX.Play();
        _shitBgSFX.Play();

        VignetteFadeController.Instance.FadeImageIn();

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
        _meetingRoomAudioSource.PlayOneShot(_makeItBackMale);
        yield return new WaitForSeconds(_makeItBackMale.length + 1f);
        
        _meetingRoomAudioSource.PlayOneShot(_myCoworkersMale);
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);
        yield return new WaitForSeconds(_myCoworkersMale.length + 1f);

        //VignetteFadeController.Instance.FadeImageOut();
        _heartbeatSlowSFX.Play();
        _runningWaterSFX.Stop();
        _footstepsEchoSFX.Stop();
        yield return new WaitForSeconds(3f);
        //VignetteFadeController.Instance.FadeImageIn();

        //_heartbeatSlowSFX.Stop();
        _meetingRoomAudioSource.PlayOneShot(_howDoIExplainMale);
        yield return new WaitForSeconds(_howDoIExplainMale.length + 1f);
        _meetingRoomAudioSource.PlayOneShot(_thisCouldRuinMale);
        yield return new WaitForSeconds(_thisCouldRuinMale.length + 1f);

        HaptticManager.Instance.StopHapticLoop();

        //Scene ending
        VignetteSceneLoadManager.Instance.LoadSceneByName("Aeroplane");
    }
    public IEnumerator StartFemaleVoices()
    {
        yield return new WaitForSeconds(2);
        _meetingRoomAudioSource.PlayOneShot(_makeItBackFemale);
        yield return new WaitForSeconds(_makeItBackFemale.length + 1f);

        _meetingRoomAudioSource.PlayOneShot(_myCoworkersFemale);
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);
        yield return new WaitForSeconds(_myCoworkersFemale.length + 1f);

        //VignetteFadeController.Instance.FadeImageOut();
        _heartbeatSlowSFX.Play();
        _runningWaterSFX.Stop();
        _footstepsEchoSFX.Stop();
        yield return new WaitForSeconds(3f);
        //VignetteFadeController.Instance.FadeImageIn();

        //_heartbeatSlowSFX.Stop();
        _meetingRoomAudioSource.PlayOneShot(_howDoIExplainFemale);
        yield return new WaitForSeconds(_howDoIExplainFemale.length + 1f);
        _meetingRoomAudioSource.PlayOneShot(_thisCouldRuinFemale);
        yield return new WaitForSeconds(_thisCouldRuinFemale.length + 1f);

        HaptticManager.Instance.StopHapticLoop();

        //Scene ending

        VignetteSceneLoadManager.Instance.LoadSceneByName("Aeroplane");
    }
}
