using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderSelectionUI : MonoBehaviour
{
    [SerializeField] private Button _maleBtn;
    [SerializeField] private Button _femaleBtn;
    [SerializeField] private GameObject _TableHandler;
    [SerializeField] private GameObject _cube;
    [SerializeField] private GameData _gameData;

    private void Start()
    {
        _maleBtn.onClick.AddListener(MaleBtnClick);
        _femaleBtn.onClick.AddListener(FemaleBtnClick);
    }

    private void FemaleBtnClick()
    {
        _gameData.playerGender = GenderEnum.Female;
        gameObject.GetComponent<Canvas>().enabled = false;
        StartCoroutine(DelayTabHandler());
    }

    private void MaleBtnClick()
    {
        _gameData.playerGender = GenderEnum.Male;
        gameObject.GetComponent<Canvas>().enabled = false;
        StartCoroutine(DelayTabHandler());
    }

    private IEnumerator DelayTabHandler()
    {
        yield return new WaitForSeconds(2f);
        LobbyGameManager.Instance.SkipTutorialBtnShow();
        _TableHandler.SetActive(true);
        _cube.SetActive(true);
        LobbyGameManager.Instance.StartTutorialInformation();
        gameObject.SetActive(false);
    }
}