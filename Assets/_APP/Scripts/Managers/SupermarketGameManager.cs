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
    [SerializeField] private AudioSource _calmAudioSource;


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
    private PlayGlow _glowEffect;

    [SerializeField] private AudioClip _maleBathroomClip1;

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
    [SerializeField] private AudioClip _maleOutroClip3;
    [SerializeField] private AudioClip _femaleOutroClip1;
    [SerializeField] private AudioClip _femaleOutroClip2;
    [SerializeField] private AudioClip _femaleOutroClip3;

    [SerializeField] private GameObject _animationEffect;
    [SerializeField] private GameObject _textEffect;

    private void Start()
    {
        VignetteFadeController.Instance.FadeImageInWithAction(() =>
        {
            _backGroundEffect.gameObject.SetActive(true);
            _rollingAudio.gameObject.SetActive(true);
            _userHeartbeatSfx.gameObject.SetActive(true);
            _calmAudioSource.gameObject.SetActive(true);
            HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.RANDOMSTOMACH);
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

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);

        _doorInteraction.SetActive(true);
        _glowEffect.Glow();
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

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip3
            : _femaleBathroomClip3;
        _voiceOverAudioSource.Play();

        HaptticManager.Instance.StopHapticLoop();
        _takaAudioSource.gameObject.SetActive(true);
        StartCoroutine(StartEffect());
        StartCoroutine(StartHardEffect());

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        StartExposureBehavior();
    }

    private IEnumerator StartHardEffect()
    {
        //HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.HARD1);
        //yield return new WaitForSeconds(4f);
        //HaptticManager.Instance.StopHapticLoop();
        //HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.HARD2);
        //yield return new WaitForSeconds(3f);
        //HaptticManager.Instance.StopHapticLoop();
        //HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.HARD3);
        //yield return new WaitForSeconds(3f);
        //HaptticManager.Instance.StopHapticLoop();
        //HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.HARD4);
        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);
        yield return new WaitForSeconds(4f);
        HaptticManager.Instance.StopHapticLoop();
    }

    private IEnumerator StartEffect()
    {
        _vignetteEffect.weight = 0;
        while (_vignetteEffect.weight < 0.8)
        {
            _vignetteEffect.weight += Time.deltaTime / 1;
            yield return null;
        }
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
        _calmAudioSource.gameObject.SetActive(false);
        _shitAudioSource.gameObject.SetActive(true);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _npcStartclip;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length);
        StartCoroutine(ShowAnimationEffect());
        yield return new WaitForSeconds(2f);
        _npcMale.SetTrigger("talk");
        _npcFemale.SetTrigger("talk");
    }

    public void Npc1Talk()
    {
        _voiceOverAudioSource.clip = _npcPublicExplosureClip1;
        _voiceOverAudioSource.Play();
    }

    private IEnumerator ShowAnimationEffect()
    {
        _animationEffect.SetActive(true);
        _textEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        _animationEffect.SetActive(false);
        yield return new WaitForSeconds(4f);
        _textEffect.SetActive(false);
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
        _npcMale.gameObject.SetActive(false);
        _npcFemale.gameObject.SetActive(false);
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
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleOutroClip3
            : _femaleOutroClip3;
        _voiceOverAudioSource.Play();
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);

        //close Scene
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            // HaptticManager.Instance.StopHapticLoop();
            SceneManager.LoadSceneAsync(2);
        });
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