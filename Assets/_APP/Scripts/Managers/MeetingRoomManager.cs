using Bhaptics.SDK2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeetingRoomManager : MonoBehaviour
{
    [SerializeField] AudioSource _meetingRoomAudioSource;
    
    [Header("SFX")]
    [SerializeField] AudioSource _meetingBgSFX;
    [SerializeField] AudioSource _calmBgSFX;
    [SerializeField] AudioSource _heartbeatNormalSFX;
    [SerializeField] AudioSource _heartbeatLouderSFX;
    [SerializeField] AudioSource _heartbeatSlowSFX;
    [SerializeField] AudioSource _footSteps;


    [Header("Halfway Presentation")]
    [SerializeField] AudioClip _halfwayPresentationMale;
    [SerializeField] AudioClip _halfwayPresentationFemale;

    [Header("Oh No")]
    [SerializeField] AudioClip _ohNoMale;
    [SerializeField] AudioClip _ohNoFemale;
    
    [Header("This Can't Happen")]
    [SerializeField] AudioClip _thisCantHappenMale;
    [SerializeField] AudioClip _thisCantHappenFemale;
    
    [Header("How Do I Leave")]
    [SerializeField] AudioClip _howDoILeaveMale;
    [SerializeField] AudioClip _howDoILeaveFemale;
    
    [Header("This Is Unpro")]
    [SerializeField] AudioClip _thisIsUnprofessionelMale;
    [SerializeField] AudioClip _thisIsUnprofessionelFemale;
 
    [SerializeField] GameData _gameData;
    void Start()
    {
        //_meetingBgSFX.Play();
        _calmBgSFX.Play();
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
        yield return new WaitForSeconds(5);

        _heartbeatNormalSFX.Play();

        _meetingRoomAudioSource.PlayOneShot(_halfwayPresentationMale);
        yield return new WaitForSeconds(_halfwayPresentationMale.length + 1f);

        _heartbeatNormalSFX.Stop();
        _heartbeatLouderSFX.Play();

        _meetingRoomAudioSource.PlayOneShot(_ohNoMale);
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.RANDOMSTOMACH);  
        yield return new WaitForSeconds(_ohNoMale.length + 1f);

        _meetingRoomAudioSource.PlayOneShot(_thisCantHappenMale);
        yield return new WaitForSeconds(_thisCantHappenMale.length + 1f);

        _heartbeatLouderSFX.Stop();
        _heartbeatSlowSFX.Play();

        _meetingRoomAudioSource.PlayOneShot(_howDoILeaveMale);
        yield return new WaitForSeconds(_howDoILeaveMale.length + 1f);
        
        _meetingRoomAudioSource.PlayOneShot(_thisIsUnprofessionelMale);
        yield return new WaitForSeconds(_thisIsUnprofessionelMale.length + 1f);

        HaptticManager.Instance.StopHapticLoop();
        VignetteFadeController.Instance.FadeImageOut();
        _footSteps.Play();
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);  
        
        yield return new WaitForSeconds(3f);
        HaptticManager.Instance.StopHapticLoop();

        SceneManager.LoadSceneAsync("CorporateBathroom");

    }
    public IEnumerator StartFemaleVoices()
    {
        yield return new WaitForSeconds(5);

        _heartbeatNormalSFX.Play();

        _meetingRoomAudioSource.PlayOneShot(_halfwayPresentationFemale);
        yield return new WaitForSeconds(_halfwayPresentationFemale.length + 1f);

        _heartbeatNormalSFX.Stop();
        _heartbeatLouderSFX.Play();

        _meetingRoomAudioSource.PlayOneShot(_ohNoFemale);
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.RANDOMSTOMACH);
        yield return new WaitForSeconds(_ohNoFemale.length + 1f);

        _meetingRoomAudioSource.PlayOneShot(_thisCantHappenFemale);
        yield return new WaitForSeconds(_thisCantHappenFemale.length + 1f);

        _heartbeatLouderSFX.Stop();
        _heartbeatSlowSFX.Play();

        _meetingRoomAudioSource.PlayOneShot(_howDoILeaveFemale);
        yield return new WaitForSeconds(_howDoILeaveFemale.length + 1f);

        _meetingRoomAudioSource.PlayOneShot(_thisIsUnprofessionelFemale);
        yield return new WaitForSeconds(_thisIsUnprofessionelFemale.length + 1f);

        HaptticManager.Instance.StopHapticLoop();
        VignetteFadeController.Instance.FadeImageOut();
        _footSteps.Play();
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);

        yield return new WaitForSeconds(3f);
        HaptticManager.Instance.StopHapticLoop();


        SceneManager.LoadSceneAsync("CorporateBathroom");

    }
}
