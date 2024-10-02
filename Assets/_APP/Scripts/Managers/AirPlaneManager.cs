using Bhaptics.SDK2;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class AirPlaneManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _playerXr;
   
    [SerializeField] private Volume _vignetteEffect;

    [SerializeField] private AudioSource _backGroundEffect;
    [SerializeField] private AudioSource _rollingAudio;
    [SerializeField] private AudioSource _userHeartbeatSfx;
    [SerializeField] private AudioSource _voiceOverAudioSource;
    [SerializeField] private AudioSource _voiceOverflightAudioSource;
    [SerializeField] private AudioSource _voiceOverNPC1AudioSource;
    [SerializeField] private AudioSource _voiceOverNPC2AudioSource;
    [SerializeField] private AudioSource _turbulanceAudioSource;

    [SerializeField] private AudioSource _calm;
    [SerializeField] private AudioSource _taka;
    [SerializeField] private AudioSource _shit;

    [SerializeField] private Transform _bathroomTransitionPosition;
    [SerializeField] private Transform _seatTransitionPosition;


    [Header("Heartbeat Clips")]
    [SerializeField]
    private AudioClip _slowHeartbeat;

    [SerializeField] private AudioClip _normalHeartbeat;
    [SerializeField] private AudioClip _speedHeartbeat;

    [SerializeField] private AudioClip _chatterAudio;
    [SerializeField] private AudioClip _intheAirPlaneAudio;

    [Header("introduction ")]
    [SerializeField] private AudioClip _turbulance;
    [SerializeField]
    private AudioClip _maleIntroductionClip1;

    [SerializeField] private AudioClip _maleIntroductionClip2;
    [SerializeField] private AudioClip _maleIntroductionClip3;

    [SerializeField] private AudioClip _femaleIntroductionClip1;
    [SerializeField] private AudioClip _femaleIntroductionClip2;
    [SerializeField] private AudioClip _femaleIntroductionClip3;

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
    [SerializeField] private AudioClip _flightAttendantClip;
    [SerializeField] private AudioClip _muffeledNoiseClip;

    [Header("Public Exposure Clips")]
    [SerializeField]
    private AudioClip _malePublicExplosureClip1;

    [SerializeField] private AudioClip _malePublicExplosureClip2;

    [SerializeField] private AudioClip _femalePublicExplosureClip1;
    [SerializeField] private AudioClip _femalePublicExplosureClip2;

    [SerializeField] private AudioClip _npcPublicExplosureClip1;
    [SerializeField] private AudioClip _npcPublicExplosureClip2;
    [SerializeField] private AudioClip _npcMurmurlls;

    [SerializeField]
    private Animator _flighAttendantAnim;

    private void Start()
    {
        _flighAttendantAnim.gameObject.SetActive(false);
        StartCoroutine(StartIntroductionBehavior());
    }

    private IEnumerator StartIntroductionBehavior()
    {
        _backGroundEffect.clip = _intheAirPlaneAudio;
        _backGroundEffect.Play();
        yield return new WaitForSeconds(2f);

        _turbulanceAudioSource.clip = _turbulance;
        _turbulanceAudioSource.Play();
        yield return new WaitForSeconds(2f);

        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.RANDOMSTOMACH);
        _userHeartbeatSfx.clip = _speedHeartbeat;
        _userHeartbeatSfx.Play();
        yield return new WaitForSeconds(1f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleIntroductionClip1
            : _femaleIntroductionClip1;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);

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
        _flighAttendantAnim.gameObject.SetActive(true);

        VignetteFadeController.Instance.FadeImageInWithAction(() =>
        {
            AudioSourcesAction(true);
            StartCoroutine(StartBathroomBehavior());
        });
    }

    private IEnumerator StartBathroomBehavior()
    {

        _backGroundEffect.clip = _chatterAudio;
        _backGroundEffect.Play();
        yield return new WaitForSeconds(2f);
        _flighAttendantAnim.SetTrigger("Shout");
        yield return new WaitForSeconds(1.7f);

        _voiceOverflightAudioSource.clip = _flightAttendantClip;
        _voiceOverflightAudioSource.Play();
        yield return new WaitForSeconds(_voiceOverflightAudioSource.clip.length + 1f);
        _flighAttendantAnim.SetTrigger("Idle");

        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _maleBathroomClip1
            : _femaleBathroomClip1;
        _voiceOverAudioSource.Play();

        HaptticManager.Instance.PlayHapticLoop(BhapticsEvent.FULLSTOMACH);
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
             ? _maleBathroomClip2
    : _femaleBathroomClip2;
        _voiceOverAudioSource.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
             ? _maleBathroomClip3 : _femaleBathroomClip3;
        _voiceOverAudioSource.Play();
       
        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        
        StartCoroutine(StartEffect());
        _taka.gameObject.SetActive(true);

        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
             ? _maleBathroomClip4 : _femaleBathroomClip4;
        _voiceOverAudioSource.Play();

        _calm.gameObject.SetActive(false);
        _shit.gameObject.SetActive(true);

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            StartCoroutine(StartExposureBehavior());
            AdjustPlayerPosition(_seatTransitionPosition);

            AudioSourcesAction(false);
        });
        //open door action
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

    public IEnumerator OnBathroomKeypadInteraction()
    {

        yield return new WaitForSeconds(_voiceOverNPC1AudioSource.clip.length + 1f);
        _voiceOverNPC1AudioSource.clip = _npcPublicExplosureClip1;
        _voiceOverNPC1AudioSource.Play();

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
            //  _backGroundEffect.clip = _chatterAudio;
            _rollingAudio.gameObject.SetActive(false);
            AudioSourcesAction(true);
            StartCoroutine(ExposureBehavior());
        });
    }

    private IEnumerator ExposureBehavior()
    {
        _backGroundEffect.clip = _npcMurmurlls;
        _backGroundEffect.Play();
        yield return new WaitForSeconds(2f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _malePublicExplosureClip1
            : _femalePublicExplosureClip1;
        _voiceOverAudioSource.Play();



        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        _voiceOverNPC1AudioSource.clip = _npcPublicExplosureClip1;
        _voiceOverNPC1AudioSource.Play();

        yield return new WaitForSeconds(_voiceOverNPC1AudioSource.clip.length + 1f);
        _voiceOverNPC2AudioSource.clip = _npcPublicExplosureClip2;
        _voiceOverNPC2AudioSource.Play();

        yield return new WaitForSeconds(_voiceOverNPC2AudioSource.clip.length + 1f);
        _voiceOverAudioSource.clip = _gameData.playerGender == GenderEnum.Male
            ? _malePublicExplosureClip2
            : _femalePublicExplosureClip2;
        _voiceOverAudioSource.Play();

        _userHeartbeatSfx.clip = _slowHeartbeat;
        _userHeartbeatSfx.Play();

        yield return new WaitForSeconds(_voiceOverAudioSource.clip.length + 1f);
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            LoadLobby();
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
    public void LoadLobby()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
