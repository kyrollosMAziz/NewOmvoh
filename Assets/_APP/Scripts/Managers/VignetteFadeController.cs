using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;


public class VignetteFadeController : SceneContextSingleton<VignetteFadeController>
{
    [SerializeField] private float _fadeDuration = 1.0f;
    [SerializeField] private Volume _vignetteEffect;
    public float FadeDuration => _fadeDuration;

    #region Fade_Controller

    public void FadeImageIn()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeImageOut()
    {
        StartCoroutine(FadeOut());
    }

    public void FadeImageInWithAction(UnityAction onFinish)
    {
        StartCoroutine(FadeIn(onFinish));
    }

    public void FadeImageOutWithAction(UnityAction onFinish)
    {
        StartCoroutine(FadeOut(onFinish));
    }

    private IEnumerator FadeIn(UnityAction onFinish = null)
    {
        _vignetteEffect.weight = 1;
        while (_vignetteEffect.weight > 0)
        {
            _vignetteEffect.weight -= Time.deltaTime / _fadeDuration;
            yield return null;
        }

        onFinish?.Invoke();
    }

    private IEnumerator FadeOut(UnityAction onFinish = null)
    {
        _vignetteEffect.weight = 0;
        while (_vignetteEffect.weight < 1)
        {
            _vignetteEffect.weight += Time.deltaTime / _fadeDuration;
            yield return null;
        }

        onFinish?.Invoke();
    }

    #endregion
}