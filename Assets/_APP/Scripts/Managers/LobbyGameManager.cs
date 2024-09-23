using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class LobbyGameManager : SceneContextSingleton<LobbyGameManager>
{

    [SerializeField] private GameObject _buttonToSkipTutorial;
    [SerializeField] private AudioSource _TutorialAudioSource;
    [SerializeField] private AudioClip _TutorialInfromationClip;
    [SerializeField] private AudioClip _GuideTutorial;
    [SerializeField] private XRGrabInteractable _cubeToHold;

    public void SkipTutorialBtnShow()
    {
        StartCoroutine(SkipTutorial());
    }

    private IEnumerator SkipTutorial()
    {
        yield return new WaitForSeconds(5f);
        _buttonToSkipTutorial.SetActive(true);
    }

    public void StartTutorialInformation()
    {
        _TutorialAudioSource.PlayOneShot(_TutorialInfromationClip);
        StartCoroutine(ShowOutline());
    }

    private IEnumerator ShowOutline()
    {
        yield return new WaitForSeconds(_TutorialInfromationClip.length);
        _TutorialAudioSource.PlayOneShot(_GuideTutorial);
        yield return new WaitForSeconds(_GuideTutorial.length);
        _cubeToHold.enabled = true;
    }
}
