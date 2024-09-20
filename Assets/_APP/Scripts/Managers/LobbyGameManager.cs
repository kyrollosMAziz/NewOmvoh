using System.Collections;
using UnityEngine;

public class LobbyGameManager : SceneContextSingleton<LobbyGameManager>
{
    [SerializeField] private GameObject _buttonToSkipTutorial;

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
    }
}