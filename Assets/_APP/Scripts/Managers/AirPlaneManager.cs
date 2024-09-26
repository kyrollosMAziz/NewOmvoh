using Bhaptics.SDK2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _playerXr;

    [SerializeField] private AudioSource _backGroundEffect;
    [SerializeField] private AudioSource _rollingAudio;
    [SerializeField] private AudioSource _userHeartbeatSfx;
    [SerializeField] private AudioSource _voiceOverAudioSource;

    [SerializeField] private Transform _bathroomTransitionPosition;


    [Header("Heartbeat Clips")]
    [SerializeField]
    private AudioClip _slowHeartbeat;

    [SerializeField] private AudioClip _normalHeartbeat;
    [SerializeField] private AudioClip _speedHeartbeat;

    [SerializeField] private AudioClip _chatterAudio;

    [Header("introduction ")]
    [SerializeField]
    private AudioClip _maleIntroductionClip1;

    [SerializeField] private AudioClip _maleIntroductionClip2;
    [SerializeField] private AudioClip _maleIntroductionclip3;

    [SerializeField] private AudioClip _femaleIntroductionClip1;
    [SerializeField] private AudioClip _femaleIntroductionClip2;
    [SerializeField] private AudioClip _femaleIntroductionclip3;

    [Header("Bathroom Audio Clips")]
    [SerializeField]
    private AudioClip _maleBathroomClip1;

    [SerializeField] private AudioClip _maleBathroomClip2;
    [SerializeField] private AudioClip _maleBathroomClip3;
    [SerializeField] private AudioClip _maleBathroomClip4;

    [SerializeField] private AudioClip _femaleBathroomClip1;
    [SerializeField] private AudioClip _femaleBathroomClip2;
    [SerializeField] private AudioClip _femaleBathroomClip3;
    [SerializeField] private AudioClip _femaleBathroomClip4;

    [Header("Public Exposure Clips")]
    [SerializeField]
    private AudioClip _malePublicExplosureClip1;

    [SerializeField] private AudioClip _malePublicExplosureClip2;

    [SerializeField] private AudioClip _femalePublicExplosureClip1;
    [SerializeField] private AudioClip _femalePublicExplosureClip2;

    [SerializeField] private AudioClip _npcPublicExplosureClip1;
    [SerializeField] private AudioClip _npcPublicExplosureClip2;
    private void Start()
    {
        StartCoroutine(StartIntroductionBehavior());
    }

    private IEnumerator StartIntroductionBehavior()
    {
        yield return new WaitForSeconds(2f);
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _maleIntroductionClip1
            : _femaleIntroductionClip1);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.RANDOMSTOMACH);
        _userHeartbeatSfx.clip = _speedHeartbeat;
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _maleIntroductionClip2
            : _femaleIntroductionClip2);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _maleIntroductionClip2
            : _femaleIntroductionClip2);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);

        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            HaptticManager.Instance.StopHapticLoop();
            AudioSourcesAction(false);
            AdjustPlayerPosition(_bathroomTransitionPosition);
            PrepareBathroomBehavior();
        });
    }
    private void PrepareBathroomBehavior()
    {
        VignetteFadeController.Instance.FadeImageInWithAction(() =>
        {
            AudioSourcesAction(true);
            StartCoroutine(StartBathroomBehavior());
        });
    }

    private IEnumerator StartBathroomBehavior()
    {
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip1
            : _femaleBathroomClip1);

        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);

        //open door action
    }

    public IEnumerator OnBathroomKeypadInteraction()
    {
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip2
            : _femaleBathroomClip2);

        _userHeartbeatSfx.volume = 1;
        _backGroundEffect.gameObject.SetActive(false);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip3
            : _femaleBathroomClip3);
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            StartCoroutine(StartExposureBehavior());
            AudioSourcesAction(false);
        });
    }

    private IEnumerator StartExposureBehavior()
    {
        yield return new WaitForSeconds(7f);
        VignetteFadeController.Instance.FadeImageInWithAction(() =>
        {
            _backGroundEffect.clip = _chatterAudio;
            _rollingAudio.gameObject.SetActive(false);
            AudioSourcesAction(true);
            StartCoroutine(ExposureBehavior());
        });
    }

    private IEnumerator ExposureBehavior()
    {
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _malePublicExplosureClip1
            : _femalePublicExplosureClip1);
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.PlayOneShot(_npcPublicExplosureClip1);
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.PlayOneShot(_npcPublicExplosureClip2);

        _userHeartbeatSfx.clip = _slowHeartbeat;
        _userHeartbeatSfx.Play();
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.PlayOneShot(_gameData.playerGender == GenderEnum.Male
            ? _malePublicExplosureClip2
            : _femalePublicExplosureClip2);


        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            VignetteFadeController.Instance.FadeImageInWithAction(() => { StartCoroutine(StartOutroBehavior()); });
        });
    }

    private IEnumerator StartOutroBehavior()
    {
        _backGroundEffect.gameObject.SetActive(false);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _userHeartbeatSfx.gameObject.SetActive(false);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        //close Scene
        HaptticManager.Instance.StopHapticLoop();
    }

    private void AdjustPlayerPosition(Transform targetTransform)
    {
        _playerXr.transform.position = targetTransform.position;
        _playerXr.transform.rotation = targetTransform.rotation;
    }

    private void AudioSourcesAction(bool flag)
    {
        _backGroundEffect.gameObject.SetActive(flag);
        _voiceOverAudioSource.gameObject.SetActive(flag);
    }
}
