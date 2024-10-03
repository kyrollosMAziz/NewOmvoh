using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject _gameplay;
    [SerializeField] private GameObject _transition;
    [SerializeField] private Material _envSkybox;
    [SerializeField] private Material _transitionSkybox;
    [SerializeField] private string _playerMaleName;
    [SerializeField] private string _playerFemaleName;
    [SerializeField] private GameData _gameData;
    private void Awake()
    {
        textMeshProUGUI.text = _gameData.playerGender == GenderEnum.Male? _playerMaleName : _playerFemaleName;
        RenderSettings.skybox = _transitionSkybox;
    }
    private void Start()
    {
        StartCoroutine(StartTranisition());
    }

    private IEnumerator StartTranisition()
    {
        yield return new WaitForSeconds(3f);
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            RenderSettings.skybox = _envSkybox;
            _gameplay.SetActive(true);
            _transition.SetActive(false);

        });
    }
}
