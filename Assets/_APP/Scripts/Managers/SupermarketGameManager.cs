using System;
using System.Collections;
using Bhaptics.SDK2;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class SupermarketGameManager : SceneContextSingleton<SupermarketGameManager>
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _playerXr;
    [SerializeField] private GameObject _doorInteraction;
    [SerializeField] private AudioSource _backGroundEffect;
    [SerializeField] private AudioSource _rollingAudio;
    [SerializeField] private AudioSource _userHeartbeatSfx;
    [SerializeField] private AudioSource _voiceOverAudioSource;
   
    [SerializeField] private AudioSource _shitAudioSource;
    [SerializeField] private AudioSource _takaAudioSource;

    
    [SerializeField] private Transform _bathroomTransitionPosition;
    [SerializeField] private Volume _vignetteEffect;

    [Header("Heartbeat Clips")] [SerializeField]
    private AudioClip _slowHeartbeat;

    [SerializeField] private AudioClip _normalHeartbeat;
    [SerializeField] private AudioClip _speedHeartbeat;

    [Header("Shopping Audio Clips")] [SerializeField]
    private AudioClip _maleIntroductionClip1;

    [SerializeField] private AudioClip _maleIntroductionClip2;
    [SerializeField] private AudioClip _maleIntroductionClip3;

    [SerializeField] private AudioClip _femaleIntroductionClip1;
    [SerializeField] private AudioClip _femaleIntroductionClip2;
    [SerializeField] private AudioClip _femaleIntroductionClip3;

    [Header("Bathroom Audio Clips")] [SerializeField]
    private AudioClip _maleBathroomClip1;

    [SerializeField] private AudioClip _maleBathroomClip2;
    [SerializeField] private AudioClip _maleBathroomClip3;

    [SerializeField] private AudioClip _femaleBathroomClip1;
    [SerializeField] private AudioClip _femaleBathroomClip2;
    [SerializeField] private AudioClip _femaleBathroomClip3;

    [Header("Public Exposure Clips")] [SerializeField]
    private AudioClip _malePublicExplosureClip1;

    [SerializeField] private AudioClip _malePublicExplosureClip2;

    [SerializeField] private AudioClip _femalePublicExplosureClip1;
    [SerializeField] private AudioClip _femalePublicExplosureClip2;

    [SerializeField] private Animator _npcMale;
    [SerializeField] private Animator _npcFemale;
    [SerializeField] private AudioClip _npcStartclip;
    [SerializeField] private AudioClip _npcPublicExplosureClip1;
    [SerializeField] private AudioClip _npcPublicExplosureClip2;
    [SerializeField] private AudioClip _npcPublicExplosureClip3;
    [SerializeField] private AudioClip _npcPublicExplosureClip4;

    [Header("Outro Clips")] [SerializeField]
    private AudioClip _maleOutroClip1;

    [SerializeField] private AudioClip _maleOutroClip2;

    [SerializeField] private AudioClip _femaleOutroClip1;
    [SerializeField] private AudioClip _femaleOutroClip2;

    private void Start()
    {
        VignetteFadeController.Instance.FadeImageInWithAction(() =>
        {
            _backGroundEffect.gameObject.SetActive(true);
            _rollingAudio.gameObject.SetActive(true);
            _userHeartbeatSfx.gameObject.SetActive(true);
            StartCoroutine(StartIntroductionBehavior());
        });
    }

    private IEnumerator StartIntroductionBehavior()
    {
        yield return new WaitForSeconds(2f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleIntroductionClip1
            : _femaleIntroductionClip1;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.RANDOMSTOMACH);
        _userHeartbeatSfx.clip = _speedHeartbeat;
        _userHeartbeatSfx.Play();

        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleIntroductionClip2
            : _femaleIntroductionClip2;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleIntroductionClip3
            : _femaleIntroductionClip3;
        _voiceOverAudioSource.Play();

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
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip1
            : _femaleBathroomClip1;
        _voiceOverAudioSource.Play();

        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);

        _doorInteraction.SetActive(true);
    }

    public void BathroomInteractionFired()
    {
        StartCoroutine(OnBathroomKeypadInteraction());
    }

    private IEnumerator OnBathroomKeypadInteraction()
    {
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip2
            : _femaleBathroomClip2;
        _voiceOverAudioSource.Play();

        _userHeartbeatSfx.volume = 1;
        _backGroundEffect.gameObject.SetActive(false);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip3
            : _femaleBathroomClip3;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _takaAudioSource.gameObject.SetActive(true);
        _vignetteEffect.weight = 0;
        while (_vignetteEffect.weight < 0.6)
        {
            _vignetteEffect.weight += Time.deltaTime / 1;
            yield return null;
        }
        StartExposureBehavior();
    }

    #region Exposure Behavior

    private void StartExposureBehavior()
    {
        _rollingAudio.gameObject.SetActive(false);
        StartCoroutine(ExposureBehavior());
    }

    private IEnumerator ExposureBehavior()
    {
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _malePublicExplosureClip1
            : _femalePublicExplosureClip1;
        _voiceOverAudioSource.Play();
        _vignetteEffect.weight = 0;
        _shitAudioSource.gameObject.SetActive(true);
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _npcStartclip;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length);
        _npcMale.SetTrigger("talk");
        _npcFemale.SetTrigger("talk");
    }

    public void Npc1Talk()
    {
        _voiceOverAudioSource.clip = _npcPublicExplosureClip1;
        _voiceOverAudioSource.Play();
    }

    public void Npc2Talk()
    {
        _voiceOverAudioSource.clip = _npcPublicExplosureClip2;
        _voiceOverAudioSource.Play();

        _userHeartbeatSfx.clip = _slowHeartbeat;
        _userHeartbeatSfx.Play();
    }

    public void Npc3Talk()
    {
        _voiceOverAudioSource.clip = _npcPublicExplosureClip3;
        _voiceOverAudioSource.Play();
    }

    public void Npc4Talk()
    {
        _voiceOverAudioSource.clip = _npcPublicExplosureClip4;
        _voiceOverAudioSource.Play();
        StartCoroutine(UserTalking());
    }


    private IEnumerator UserTalking()
    {
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _malePublicExplosureClip2
            : _femalePublicExplosureClip2;
        _voiceOverAudioSource.Play();
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        VignetteFadeController.Instance.FadeImageOutWithAction(() => { StartCoroutine(StartOutroBehavior()); });
    }

    #endregion

    #region Outro Behavior

    private IEnumerator StartOutroBehavior()
    {
        yield return new WaitForSeconds(4);
        VignetteFadeController.Instance.FadeImageInWithAction(() =>
        {
            _backGroundEffect.gameObject.SetActive(false);
            _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
                ? _maleOutroClip1
                : _femaleOutroClip1;
            _voiceOverAudioSource.Play();
        });
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _userHeartbeatSfx.gameObject.SetActive(false);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleOutroClip2
            : _femaleOutroClip2;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        //close Scene
        HaptticManager.Instance.StopHapticLoop();
        VignetteFadeController.Instance.FadeImageOutWithAction(() => { SceneManager.LoadSceneAsync(2); });
    }

    #endregion

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