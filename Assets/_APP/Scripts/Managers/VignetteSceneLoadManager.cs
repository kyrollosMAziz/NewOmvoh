
using UnityEngine.SceneManagement;

public class VignetteSceneLoadManager : SceneContextSingleton<VignetteSceneLoadManager>
{
    public void LoadSceneByName(string sceneName)
    {
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            SceneManager.LoadSceneAsync(sceneName);
        });
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        VignetteFadeController.Instance.FadeImageOutWithAction(() =>
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        });
    }
}